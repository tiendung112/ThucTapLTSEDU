using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThucTapLTSEDU.Pagination;
using ThucTapLTSEDU.PayLoads.DTOs;
using ThucTapLTSEDU.Services.Implements;
using ThucTapLTSEDU.Services.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ThucTapLTSEDU.PayLoads.Requests.Auth;


namespace ThucTapLTSEDU.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly IAuthServices authServices;
        public AuthController(IConfiguration configuration)
        {
            authServices = new AuthServices(configuration);

            _configuration = configuration;
        }

        [HttpPost]
        [Route("/API/Auth/DangKyTaiKhoan")]
        public async Task<IActionResult> DangKyTaiKhoan([FromForm]Request_DangKyTaiKhoan request)
        {
            var result =await authServices.DangKyTaiKhoan(request);
            if (result == null)
            {
                return NotFound();

            }
            else
            {
                return Ok(result);
            }
        }
        [HttpPost]
        [Route("/api/auth/renew-token")]
        [Authorize(Roles ="ADMIN")]
        public IActionResult RenewToken(TokenDTOs token)
        {
            var result = authServices.RenewAccessToken(token);
            if (result == null)
            {
                return Unauthorized(result);
            }
            return Ok(result);
        }

        [HttpPut]
        [Route("/api/auth/change-password")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]     
        public async Task<IActionResult> ChangePassword(Request_ChangePassword request)
        {
            int id = Convert.ToInt32(HttpContext.User.FindFirst("Id").Value);
            return Ok(await authServices.ChangePassword(id, request));

        }

        [HttpPut]
        [Route("/API/Auth/XacNhanTaiKhoan")]
        public async Task<IActionResult> XacNhanTaiKhoan([FromForm] Request_ValidateRegister request)
        {
            var result = await authServices.XacNhanDangKyTaiKhoan(request);
            if (result == null)
            {
                return NotFound(result);

            }
            else
            {
                return Ok(result);
            }
        }
        [HttpDelete]
        [Route("/API/Auth/XoaTaiKhoanChuaKichHoat")]
        [Authorize(Roles ="ADMIN")]
        public async Task<IActionResult> XoaTaiKhoanChuaKichHoat()
        {
            var result = await authServices.RemoveTKNotActive();
            if (result == null)
            {
                return NotFound(result);

            }
            else
            {
                return Ok(result);
            }
        }
        [HttpPost]
        [Route(("/api/auth/login"))]
        public async Task<IActionResult> Login(Request_Login request)
        {
            var result = await authServices.Login(request);
            if (result == null)
            {
                return Unauthorized(result);
            }
            return Ok(result);
        }
        [HttpPost]
        [Route(("/api/auth/GetALL"))]
        [Authorize(Roles ="ADMIN")]
        public async Task<IActionResult> GetALL()
        {
            Pagintation pagintation = new Pagintation();
            var result = await authServices.GetAlls(pagintation);
            if (result == null)
            {
                return Unauthorized(result);
            }
            return Ok(result);
        }
    }
}
