using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using ThucTapLTSEDU.Pagination;
using ThucTapLTSEDU.PayLoads.Requests.Auth;
using ThucTapLTSEDU.PayLoads.Requests.Orders;
using ThucTapLTSEDU.Services.Implements;
using ThucTapLTSEDU.Services.IServices;

namespace ThucTapLTSEDU.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrder_Services services;
        public OrderController()
        {
            services = new Order_Services();
        }
        [HttpPost]
        [Route("/api/Order/Them")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> TaoOrder(Request_ThemOrder order)
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            var res = await services.TaoOder(id, order);
            return Ok(res);

        }
        [HttpPut]
        [Route("/api/Order/XacNhanOrder")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> TaoOrder(Request_ValidateRegister order)
        {
            var res = await services.XacNhanOrder(order);
            return Ok(res);
        }

        [HttpPut]
        [Route("/api/Order/SuaOrder/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> TaoOrder([FromRoute] int id, Request_SuaOrder order)
        {
            var res = await services.SuaOder(id, order);
            return Ok(res);
        }

        [HttpDelete]
        [Route("/api/Order/XoaOrder/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> XoaOrder([FromRoute] int id)
        {
            var res = await services.XoaOder(id);
            return Ok(res);

        }
        [HttpDelete]
        [Route("/api/Order/XoaOrderChuaKichHoat/{id}")]
        [Authorize(Roles ="ADMIN")]
        public async Task<IActionResult> XoaOrderChuaKichHoat([FromRoute] int id)
        {
            var res = await services.XoaOder(id);
            return Ok(res);
        }
        [HttpGet]
        [Route("/api/Order/HienThi")]
        public async Task<IActionResult> HienThiDanhSachSanPham(int pageSize, int pageNumber)
        {
            Pagintation pagintation1 = new Pagintation() { PageSize = pageSize, PageNumber = pageNumber };
            return Ok(services.getAll(pagintation1));
        }
    }
}
