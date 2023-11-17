using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ThucTapLTSEDU.Entities;
using ThucTapLTSEDU.Handler.Email;
using ThucTapLTSEDU.Handler.Image;
using ThucTapLTSEDU.Pagination;
using ThucTapLTSEDU.Payloads.Responses;
using ThucTapLTSEDU.PayLoads.Converters;
using ThucTapLTSEDU.PayLoads.DTOs;
using ThucTapLTSEDU.PayLoads.Requests;
using ThucTapLTSEDU.Services.IServices;
using BCryptNet = BCrypt.Net.BCrypt;
namespace ThucTapLTSEDU.Services.Implements
{
    public class AuthServices : BaseServices, IAuthServices
    {
        private readonly IConfiguration _configuration;
        private readonly User_Converters converters_User;
        private readonly Account_Converters converters_acc;
        private readonly ResponseObject<AccountDTOs> response;
        private readonly ResponseObject<TokenDTOs> responseObjectToken;
        public AuthServices(IConfiguration configuration)
        {
            _configuration = configuration;
            converters_User = new User_Converters();
            converters_acc = new Account_Converters();
            response = new ResponseObject<AccountDTOs>();
            responseObjectToken = new ResponseObject<TokenDTOs> ();
        }
        public async Task<ResponseObject<AccountDTOs>> DangKyTaiKhoan(Request_DangKyTaiKhoan request)
        {
            if (string.IsNullOrWhiteSpace(request.username)
               || string.IsNullOrWhiteSpace(request.password)
               || string.IsNullOrWhiteSpace(request.phone)
               || string.IsNullOrWhiteSpace(request.email)
               || string.IsNullOrWhiteSpace(request.address))
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "Chưa điền đầy đủ thông tin ", null);
            }
            if (Validate.IsValidEmail(request.email) == false)
            {
                return response.ResponseError(StatusCodes.Status400BadRequest, "Định dạng Email không hợp lệ", null);
            }
            if (await context.Users.FirstOrDefaultAsync(x => x.user_name.Equals(request.username)) != null)
            {
                return response.ResponseError(StatusCodes.Status400BadRequest, "Tên tài khoản đã tồn tại trên hệ thống", null);
            }
            if (await context.Users.FirstOrDefaultAsync(x => x.email.Equals(request.email)) != null)
            {
                return response.ResponseError(StatusCodes.Status400BadRequest, "Email đã tồn tại trên hệ thống", null);
            }
            else
            {
                int imageSize = 2 * 1024 * 768;
                try
                {
                    Account account = new Account();
                    account.user_name = request.username;
                    account.password = BCryptNet.HashPassword(request.password);
                    if (request.avatar != null)
                    {
                        if (!HandleImage.IsImage(request.avatar, imageSize))
                        {
                            return response.ResponseError(StatusCodes.Status400BadRequest, "Ảnh không hợp lệ", null);
                        }
                        else
                        {
                            var avatarFile = await HandleUploadImage.Upfile(request.avatar);
                            account.avatar = avatarFile == "" ? "https://media.istockphoto.com/id/1300845620/vector/user-icon-flat-isolated-on-white-background-user-symbol-vector-illustration.jpg?s=612x612&w=0&k=20&c=yBeyba0hUkh14_jgv1OKqIH0CCSWU_4ckRkAoy2p73o=" : avatarFile;
                        }
                    }
                    account.status = 0;
                    account.decedecentralizationID = 3;
                    account.created_at = DateTime.UtcNow;
                    await context.Accounts.AddAsync(account);
                    await context.SaveChangesAsync();


                    User user = new User();
                    user.user_name = request.name;
                    user.email = request.email;
                    user.phone = request.phone;
                    user.address = request.address;
                    user.created_at = DateTime.Now;
                    user.accountID = account.Id;                  
                    await context.Users.AddAsync(user);
                    await context.SaveChangesAsync();
                    
                    
                    //xoá hết kích hoạt liên quan đến tài khoản
                    var confrim = context.EmailValidates.Where(x => x.AccountID == account.Id);
                    context.EmailValidates.RemoveRange(confrim);
                    await context.SaveChangesAsync();

                    //gửi mail kích hoạt tài khoản
                    EmailValidate confrimEmail = new EmailValidate()
                    {
                        AccountID = account.Id,
                        DaXacNhan = false,
                        ThoiGianHetHan =DateTime.Now.AddMinutes(15),
                        MaXacNhan = GenerateCodeActive().ToString(),
                    };
                    await context.EmailValidates.AddAsync(confrimEmail);
                    await context.SaveChangesAsync();
                    string mss = SendEmail(new EmailTo
                    {
                        Mail = request.email,
                        Subject = "Nhận mã xác nhận để tạo mật khẩu mới từ đây: ",
                        Content = $"Mã kích hoạt của bạn là: {confrimEmail.MaXacNhan}, mã này sẽ hết hạn sau 15 phút"
                    }) ;
                    return response.ResponseSuccess
                        ("Bạn đã gửi yêu cầu đăng ký tài khoản," +
                        " vui lòng nhập mã xác nhận đã được gửi về email của bạn"
                        , converters_acc.EntityToDTOs(account));

                }
                catch (Exception ex)
                {
                    return response.ResponseError(StatusCodes.Status500InternalServerError, ex.Message, null);
                }
                
            }
        }

        public async Task<string> XacNhanDangKyTaiKhoan(Request_ValidateRegister request)
        {
            EmailValidate confirmEmail = await context.EmailValidates
                .Where(x => x.MaXacNhan.Equals(request.MaXacNhan))
                .FirstOrDefaultAsync(); 
             
            if (confirmEmail is null)
            {
                return "Mã xác nhận không chính xác";
            }
            if (confirmEmail.ThoiGianHetHan < DateTime.Now)
            {
                return "Mã xác nhận đã hết hạn";
            }
            Account acc =context.Accounts.FirstOrDefault(x => x.Id == confirmEmail.AccountID);
            acc.status = 1;
            acc.updated_at = DateTime.Now;
            context.EmailValidates.Remove(confirmEmail);
            context.Accounts.Update(acc);
            await context.SaveChangesAsync();
            return "Xác nhận đăng ký tài khoản thành công, vui lòng đăng nhập tài khoản của bạn";
        }
        public async Task<string> RemoveTKNotActive()
        {
            var acc = context.Accounts.Where(x => x.status == 0).ToList();
            
            foreach (var t in acc)
            {
                var confimemail = context.EmailValidates.Where(x => x.AccountID == t.Id);
                context.EmailValidates.RemoveRange(confimemail);
                var lstUser = context.Users.Where(x => x.accountID == t.Id);
                context.Users.RemoveRange(lstUser);

                DateTime? next15P = t.created_at + TimeSpan.FromMinutes(15);
                if (t.created_at < DateTime.Now)
                {
                    context.Remove(t);
                }
                
            }
            await context.SaveChangesAsync();
            return "đã xoá hết tài khoản chưa kích hoạt";
        }

        public string GenerateRefreshToken()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }

        public TokenDTOs GenerateAccessToken(Account acc)
        {
            var user = context.Users.SingleOrDefault(x => x.accountID == acc.Id);
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:SecretKey").Value!);

            var decentralization = context.Decentralizations.FirstOrDefault(x => x.Id == acc.decedecentralizationID);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", acc.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.email),
                    new Claim("Username", acc.user_name),
                    new Claim("Avatar", acc.avatar),
                    new Claim("RoleId", acc.decedecentralizationID.ToString()),
                    new Claim(ClaimTypes.Role,decentralization?.Authority_name ?? "")
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescription);
            var accessToken = jwtTokenHandler.WriteToken(token);
            var refreshToken = GenerateRefreshToken();

            RefreshToken rf = new RefreshToken
            {
                Token = refreshToken,
                ExpiredTime = DateTime.UtcNow.AddHours(3),
                accountID = acc.Id
            };

            context.RefreshTokens.Add(rf);
            context.SaveChanges();

            TokenDTOs tokenDTO = new TokenDTOs
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
            return tokenDTO;
        }

        public ResponseObject<TokenDTOs> RenewAccessToken(TokenDTOs request)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:SecretKey").Value);

            var tokenValidation = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:SecretKey").Value))
            };

            try
            {
                var tokenAuthentication = jwtTokenHandler.ValidateToken(request.AccessToken, tokenValidation, out var validatedToken);
                if (validatedToken is not JwtSecurityToken jwtSecurityToken || jwtSecurityToken.Header.Alg != SecurityAlgorithms.HmacSha256)
                {
                    return responseObjectToken.ResponseError(StatusCodes.Status400BadRequest, "Token không hợp lệ", null);
                }

                RefreshToken refreshToken = context.RefreshTokens.FirstOrDefault(x => x.Token == request.RefreshToken);
                if (refreshToken == null)
                {
                    return responseObjectToken.ResponseError(StatusCodes.Status404NotFound, "RefreshToken không tồn tại trong database", null);
                }
                if (refreshToken.ExpiredTime < DateTime.Now)
                {
                    return responseObjectToken.ResponseError(StatusCodes.Status401Unauthorized, "Token chưa hết hạn", null);
                }
                var acc = context.Accounts.FirstOrDefault(x => x.Id == refreshToken.accountID);
                if (acc == null)
                {
                    return responseObjectToken.ResponseError(StatusCodes.Status404NotFound, "Người dùng không tồn tại", null);
                }
                var newToken = GenerateAccessToken(acc);

                return responseObjectToken.ResponseSuccess("Làm mới token thành công", newToken);
            }
            catch (Exception ex)
            {
                return responseObjectToken.ResponseError(StatusCodes.Status500InternalServerError, ex.Message, null);
            }
        }

        public async Task<ResponseObject<TokenDTOs>> Login(Request_Login request)
        {
            if (string.IsNullOrWhiteSpace(request.Password) || string.IsNullOrWhiteSpace(request.UserName))
            {
                return responseObjectToken.ResponseError(StatusCodes.Status400BadRequest, "Vui lòng điền đầy đủ thông tin", null);
            }

            var acc = await context.Accounts.FirstOrDefaultAsync(x => x.user_name.Equals(request.UserName));
            if (acc is null)
            {
                return responseObjectToken.ResponseError(StatusCodes.Status404NotFound, "Tên tài khoản không tồn tại", null);
            }
            else if (acc.status == 0)
            {
                return responseObjectToken.ResponseError(StatusCodes.Status400BadRequest, "Tài khoản chưa kích hoạt", null);
            }
            bool isPasswordValid = BCryptNet.Verify(request.Password, acc.password);
            if (!isPasswordValid)
            {
                return responseObjectToken.ResponseError(StatusCodes.Status400BadRequest, "Tên đăng nhập hoặc mật khẩu không chính xác", null);
            }

            else
            {
                return responseObjectToken.ResponseSuccess("Đăng nhập thành công", GenerateAccessToken(acc));
            }
        }

        public Task<IEnumerable<AccountDTOs>> GetAlls(Pagintation pagintation)
        {
            var result= context.Accounts.Include(y => y.Users).Select(x=>converters_acc.EntityToDTOs(x)).ToList();
            return Task.FromResult<IEnumerable<AccountDTOs>>(result);
        }

        public async Task<ResponseObject<AccountDTOs>> ChangePassword(int accID, Request_ChangePassword request)
        {
            var  acc = await context.Accounts.FirstOrDefaultAsync(x => x.Id == accID);
            if (!BCryptNet.Verify(request.OldPassword, acc.password))
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "Mật khẩu cũ không chính xác", null);
            }
            acc.password = BCryptNet.HashPassword(request.NewPassword);
            context.Accounts.Update(acc);
            await context.SaveChangesAsync();
            return response.ResponseSuccess("Đối mật khẩu thành công", converters_acc.EntityToDTOs(acc));
        }

        public async Task<string> ForgotPassword(Request_ForgotPassword request)
        {
            User user = await context.Users.FirstOrDefaultAsync(x => x.email.Equals(request.Email));
            if (user is null)
            {
                return "Email không tồn tại trong hệ thống";
            }
            else
            {
                var acc = context.Accounts.SingleOrDefault(x => x.Id == user.accountID);
                if (acc is null)
                {
                    return "Tài Khoản không tồn tại trong hệ thống";
                }

                var confirms = context.EmailValidates.Where(x => x.AccountID == acc.Id).ToList();
                context.EmailValidates.RemoveRange(confirms);
                await context.SaveChangesAsync();
                EmailValidate confirmEmail = new EmailValidate
                {
                    AccountID = acc.Id,
                    DaXacNhan = false,
                    ThoiGianHetHan = DateTime.Now.AddMinutes(5),
                    MaXacNhan = GenerateCodeActive().ToString()
                };
                await context.EmailValidates.AddAsync(confirmEmail);
                await context.SaveChangesAsync();
                string message = SendEmail(new EmailTo
                {
                    Mail = request.Email,
                    Subject = "Nhận mã xác nhận để tạo mật khẩu mới từ đây: ",
                    Content = $"Mã kích hoạt của bạn là: {confirmEmail.MaXacNhan}, mã này sẽ hết hạn sau 5 phút"
                });
                return "Gửi mã xác nhận về email thành công, vui lòng kiểm tra email";
            }
        }
        public async Task<ResponseObject<AccountDTOs>> CreateNewPassword(Request_ConfirmCreateNewPassword request)
        {
            EmailValidate confirmEmail = await context.EmailValidates.Where(x => x.MaXacNhan.Equals(request.CodeActive)).FirstOrDefaultAsync();
            if (confirmEmail is null)
            {
                return response.ResponseError(StatusCodes.Status400BadRequest, "Mã xác nhận không chính xác", null);
            }
            if (confirmEmail.ThoiGianHetHan < DateTime.Now)
            {
                return response.ResponseError(StatusCodes.Status400BadRequest, "Mã xác nhận đã hết hạn", null);
            }
            Account acc = context.Accounts.FirstOrDefault(x => x.Id == confirmEmail.AccountID);
            acc.password = BCryptNet.HashPassword(request.NewPassword);
            context.EmailValidates.Remove(confirmEmail);
            context.Accounts.Update(acc);
            await context.SaveChangesAsync();
            return response.ResponseSuccess("Tạo mật khẩu mới thành công", converters_acc.EntityToDTOs(acc));

        }
    }
}
