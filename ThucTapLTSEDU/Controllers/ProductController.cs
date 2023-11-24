using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using ThucTapLTSEDU.Pagination;
using ThucTapLTSEDU.PayLoads.DTOs;
using ThucTapLTSEDU.PayLoads.Requests.Products;
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

        [HttpPost]
        [Route("/api/Product/ThemSanPham")]
        public async Task<IActionResult> ThemSanPham(Request_ThemProduct request)
        {
            return Ok(await services.ThemSanPham(request));
        }

        [HttpPut]
        [Route("/api/Product/SuaSanPham/{id}")]
        public async Task<IActionResult> SuaSanPham([FromRoute] int id,Request_SuaProduct request)
        {
            return Ok(await services.SuaSanPham(id,request));
        }

        [HttpDelete]
        [Route("/api/Product/XoaSanPham/{id}")]
        public async Task<IActionResult> XoaSanPham([FromRoute] int id)
        {
            return Ok(await services.XoaSanPham(id));
        }

        [HttpGet]
        [Route("/api/Product/HienThi")]
        public async Task<IActionResult> HienThiDanhSachSanPham(int pageSize, int pageNumber)
        {
            Pagintation pagintation1 = new Pagintation() { PageSize=pageSize,PageNumber = pageNumber};
            return Ok(services.HienThiDanhSachSanPham(pagintation1));
        }


    }
}
