using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThucTapLTSEDU.Pagination;
using ThucTapLTSEDU.PayLoads.DTOs;
using ThucTapLTSEDU.PayLoads.Requests.Product_type;
using ThucTapLTSEDU.PayLoads.Requests.Products;
using ThucTapLTSEDU.Services.Implements;
using ThucTapLTSEDU.Services.IServices;

namespace ThucTapLTSEDU.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IProduct_Services product;
        private readonly IThongKe ThongKe;
        private readonly IProductType_Services product_type;
        public AdminController() 
        {
            product = new Product_Services();
            ThongKe = new ThongKeServices();
            product_type = new ProductType_Services();
        }
        #region xử lý sản phẩm 
        [HttpPost]
        [Route("/api/Admin/Product/ThemSanPham")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> ThemSanPham(Request_ThemProduct request)
        {
            return Ok(await product.ThemSanPham(request));
        }

        [HttpPut]
        [Route("/api/Admin/Product/SuaSanPham/{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> SuaSanPham([FromRoute] int id, Request_SuaProduct request)
        {
            return Ok(await product.SuaSanPham(id, request));
        }

        [HttpDelete]
        [Route("/api/Admin/Product/XoaSanPham/{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> XoaSanPham([FromRoute] int id)
        {
            return Ok(await product.XoaSanPham(id));
        }

        
        #endregion
        #region xử lý thông kê
        [HttpGet]
        [Route("/api/Admin/ThongKe/doanhThuTheoNam/year:{nam}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> doanhThuTheoNam([FromRoute] int nam)
        {
            var result = await ThongKe.doanhThuTheoNam(nam);
            return Ok(result);
        }

        [HttpGet]
        [Route("/api/Admin/ThongKe/doanhThuTheoQuy/Quy:{quy}_Nam{nam}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> doanhThuTheoQuy([FromRoute] int quy, int nam)
        {
            var result = await ThongKe.doanhThuTheoQuy(quy, nam);
            return Ok(result);
        }

        [HttpGet]
        [Route("/api/Admin/ThongKe/doanhThuTheoThang/Thang:{thang}_Nam{nam}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> doanhThuTheoThang([FromRoute] int thang, int nam)
        {
            var result = await ThongKe.doanhThuTheoThang(thang, nam);
            return Ok(result);
        }

        #endregion

        #region xử lý loại sản phẩm 
        [HttpGet]
        [Route("/api/Admin/ProductType/HienThi")]
        public async Task<IActionResult> DisplayProudct_type(int pageSize, int pageNumber)
        {
            
            Pagintation pagintation1 = new Pagintation() { PageNumber = pageNumber,PageSize =pageSize};
            return Ok(product_type.DisplayProudct_type(pagintation1));
        }



        [HttpPost]
        [Route("/api/Admin/ProductType/CreateProductType")]
        [Authorize(Roles = "ADMIN")]    
        public async Task<IActionResult> CreateProduct_type([FromForm] Request_Create_Product_type request)
        {
            var res = await product_type.CreateProudct_type(request);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }

        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        [Route("/api/Admin/ProductType/UpdateProductType")]
        public async Task<IActionResult> UpdateProduct_type([FromForm] Request_Update_Product_type request)
        {
            var res = await product_type.UpdateProudct_type(request);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }

        [HttpDelete]
        [Authorize(Roles = "ADMIN")]
        [Route("/api/Admin/ProductType/DeletePoductType/{id}")]
        public async Task<IActionResult> DeletePoductType([FromRoute] int id  )
        {
            var res = await product_type.DeleteProudct_type(id);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }
        #endregion

    }
}
