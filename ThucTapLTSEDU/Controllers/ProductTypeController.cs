using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
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

        [HttpPost]
        [Route("/api/ProductType/CreateProductType")]
        [Authorize(Roles = "ADMIN")]    
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
    }
}
