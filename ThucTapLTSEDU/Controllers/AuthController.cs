using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThucTapLTSEDU.Pagination;
using ThucTapLTSEDU.PayLoads.DTOs;
using ThucTapLTSEDU.Services.Implements;
using ThucTapLTSEDU.Services.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ThucTapLTSEDU.PayLoads.Requests.Auth;
using ThucTapLTSEDU.PayLoads.Requests.Product_Review;
using ThucTapLTSEDU.PayLoads.Requests.Cart;

namespace ThucTapLTSEDU.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IProduct_Services product;
        private readonly IAuth_Services authServices;
        private readonly IProductReview_Services review;
        private readonly ICart_Services cartServices;
        public AuthController(IConfiguration configuration)
        {
            authServices = new Auth_Services(configuration);
            product = new Product_Services();
            _configuration = configuration;
            review = new ProductReview_Services();
            cartServices  = new Cart_Services();
        }

        #region xử lý liên quan đến tài khoản
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
        public async Task<IActionResult> Login([FromBody]Request_Login request)
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
        #endregion

        #region xử lý review 
        [HttpGet]
        [Route("/api/Review/HienThi/{id}")]
        public async Task<IActionResult> HienThiDanhGia([FromRoute] int id)
        {
            Pagintation pa = new Pagintation();
            return Ok(await review.HienThiDSReview(id, pa));
        }

        [HttpGet]
        [Route("/api/Review/HienThi/")]
        public async Task<IActionResult> HienThiDanhGia()
        {
            Pagintation pa = new Pagintation();
            return Ok(await review.HienThiDSReview(0, pa));
        }

        [HttpPost]
        [Route("/api/Review/ThemReview")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ThemReview([FromBody] Request_ThemProductReview request)
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            var res = await review.ThemReview(id, request);
            return Ok(res);
        }

        [HttpPut]
        [Route("/api/Review/SuaReview/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> SuaReview([FromRoute] int id, [FromBody] Request_SuaProductReview request)
        {
            //int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            var res = await review.SuaReview(id, request);
            return Ok(res);
        }

        [HttpDelete]
        [Route("/api/Review/XoaReview/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> XoaReview([FromRoute] int id)
        {
            var res = await review.XoaReview(id);
            return Ok(res);
        }
        #endregion

        #region xử lý sản phẩm
        [HttpGet]
        [Route("/api/Product/HienThi")]
        public async Task<IActionResult> HienThiDanhSachSanPham(int pageSize, int pageNumber)
        {
            Pagintation pagintation1 = new Pagintation() { PageSize = pageSize, PageNumber = pageNumber };
            return Ok(product.HienThiDanhSachSanPham(pagintation1));
        }
        [HttpGet]
        [Route("/api/Product/HienThiSanPhamNoiBat")]
        public async Task<IActionResult> HienThiSanPhamNoiBat()
        {
            return Ok( await product.HienThiCacSanPhamNoiBat());
        }
        [HttpGet]
        [Route("/api/Product/HienThiSP/{id}")]
        public async Task<IActionResult> HienThiSanPhamTheoID(int id )
        {
           
            return Ok(await product.HienThiSanPham(id));
        }
        #endregion

        #region xử lý đơn hàng
        [HttpPost]
        [Route("/api/Cart/ThemDonHang")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ThemDonHang(Request_ThemCart request)
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await cartServices.ThemCart(id, request));
        }
        #endregion
    }
}
