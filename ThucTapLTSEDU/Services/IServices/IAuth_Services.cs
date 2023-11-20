using ThucTapLTSEDU.Entities;
using ThucTapLTSEDU.Handler.Email;
using ThucTapLTSEDU.Pagination;
using ThucTapLTSEDU.Payloads.Responses;
using ThucTapLTSEDU.PayLoads.DTOs;
using ThucTapLTSEDU.PayLoads.Requests.Auth;

namespace ThucTapLTSEDU.Services.IServices
{
    public interface IAuth_Services
    {
        Task<ResponseObject<AccountDTOs>> DangKyTaiKhoan(Request_DangKyTaiKhoan request);
        Task<string> XacNhanDangKyTaiKhoan(Request_ValidateRegister request);
        Task<string> RemoveTKNotActive();
        string GenerateRefreshToken();
        TokenDTOs GenerateAccessToken(Account acc);
        ResponseObject<TokenDTOs> RenewAccessToken(TokenDTOs request);
        Task<ResponseObject<TokenDTOs>> Login(Request_Login request);
        Task<IEnumerable<AccountDTOs>> GetAlls(Pagintation pagintation);
        Task<ResponseObject<AccountDTOs>> ChangePassword(int accID, Request_ChangePassword request);
        string SendEmail(EmailTo emailTo);
        Task<string> ForgotPassword(Request_ForgotPassword request);
        Task<ResponseObject<AccountDTOs>> CreateNewPassword(Request_ConfirmCreateNewPassword request);
    }
}
