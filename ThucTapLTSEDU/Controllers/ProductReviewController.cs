using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThucTapLTSEDU.Pagination;
using ThucTapLTSEDU.PayLoads.Requests.Product_Review;
using ThucTapLTSEDU.Services.Implements;
using ThucTapLTSEDU.Services.IServices;

namespace ThucTapLTSEDU.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductReviewController : ControllerBase
    {
        private readonly IProductReview_Services services;
        public ProductReviewController()
        {
            services = new ProductReview_Services();
        }

        [HttpGet]
        [Route("/api/Review/HienThi/{id}")]
        public async Task<IActionResult> HienThiDanhGia([FromRoute]int id)
        {
            Pagintation pa = new Pagintation();
            return Ok(await services.HienThiDSReview(id,pa));
        }

        [HttpGet]
        [Route("/api/Review/HienThi/")]
        public async Task<IActionResult> HienThiDanhGia()
        {
            Pagintation pa = new Pagintation();
            return Ok(await services.HienThiDSReview(0, pa));
        }

        [HttpPost]
        [Route("/api/Review/ThemReview")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ThemReview([FromBody] Request_ThemProductReview request)
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            var res =await services.ThemReview(id,request);
            return Ok(res);
        }

        [HttpPut]
        [Route("/api/Review/SuaReview/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> SuaReview([FromRoute]int id ,[FromBody] Request_SuaProductReview request)
        {
            //int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            var res = await services.SuaReview(id, request);
            return Ok(res);
        }

        [HttpDelete]
        [Route("/api/Review/XoaReview/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> XoaReview([FromRoute] int id)
        {
            var res = await services.XoaReview(id);
            return Ok(res);
        }
    }
}
