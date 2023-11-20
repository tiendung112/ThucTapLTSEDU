using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using ThucTapLTSEDU.Pagination;
using ThucTapLTSEDU.PayLoads.DTOs;
using ThucTapLTSEDU.PayLoads.Requests.Product_type;
using ThucTapLTSEDU.Services.Implements;
using ThucTapLTSEDU.Services.IServices;

namespace ThucTapLTSEDU.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private readonly IProductType_Services services;
        public ProductTypeController()
        {
            services = new ProductType_Services();
        }
        [HttpGet]
        [Route("/api/ProductType/HienThi")]
        public async Task<IActionResult> DisplayProudct_type(int pageSize, int pageNumber)
        {
            if (pageNumber != 0 && pageNumber != 0)
            {
                Pagintation pagintation = new Pagintation()
                {
                    PageSize = pageSize,
                    PageNumber = pageNumber
                };
                var res = await services.DisplayProudct_type(pagintation);
                var pageRes = PageResult<Product_TypeDTOs>.toPageResult(pagintation, res);
                pagintation.TotalCount = pageRes.Count();

                var result = new PageResult<Product_TypeDTOs>(pagintation, pageRes);
                return Ok(res);
            }
            Pagintation pagintation1 = new Pagintation();
            return Ok(services.DisplayProudct_type(pagintation1));
        }



        [HttpPost]
        [Route("/api/ProductType/CreateProductType")]
        //[Authorize(Roles = "ADMIN")]    
        public async Task<IActionResult> CreateProduct_type([FromForm] Request_Create_Product_type request)
        {
            var res = await services.CreateProudct_type(request);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }

        [HttpPut]
        [Authorize(Roles ="ADMIN")]
        [Route("/api/ProductType/UpdateProductType")]
        public async Task<IActionResult> UpdateProduct_type([FromForm] Request_Update_Product_type request)
        {
            var res = await services.UpdateProudct_type(request);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }


        [HttpDelete]
        [Authorize(Roles = "ADMIN")]
        [Route("/api/ProductType/DeletePoductType")]
        public async Task<IActionResult> DeletePoductType([FromForm] Request_Delete_Product_type request)
        {
            var res = await services.DeleteProudct_type(request);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }
    }
}
