using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using ThucTapLTSEDU.Pagination;
using ThucTapLTSEDU.PayLoads.DTOs;
using ThucTapLTSEDU.Services.Implements;
using ThucTapLTSEDU.Services.IServices;

namespace ThucTapLTSEDU.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct_Services services;
        public ProductController()
        {
            services = new Product_Services();
        }

        [HttpGet]
        [Route("/api/Product/HienThi")]
        public async Task<IActionResult> HienThiDanhSachSanPham(int pageSize, int pageNumber)
        {
            if(pageNumber != 0 && pageNumber!=0) {
                Pagintation pagintation = new Pagintation() {PageSize = pageSize,
                PageNumber = pageNumber};
                var res = await services.HienThiDanhSachSanPham(pagintation);
                var pageRes = PageResult<ProductDTOs>.toPageResult(pagintation, res);
                pagintation.TotalCount = pageRes.Count();
                
                var result = new PageResult<ProductDTOs>(pagintation, pageRes);
                return Ok(res);
            }
            Pagintation pagintation1 = new Pagintation();
            return Ok(services.HienThiDanhSachSanPham(pagintation1));
        }
    }
}
