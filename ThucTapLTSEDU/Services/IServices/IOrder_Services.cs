using Microsoft.AspNetCore.Mvc.Razor.Infrastructure;
using ThucTapLTSEDU.Pagination;
using ThucTapLTSEDU.Payloads.Responses;
using ThucTapLTSEDU.PayLoads.DTOs;
using ThucTapLTSEDU.PayLoads.Requests.Auth;
using ThucTapLTSEDU.PayLoads.Requests.Orders;

namespace ThucTapLTSEDU.Services.IServices
{
    public interface IOrder_Services
    {
        Task<ResponseObject<OrderDTOs>> TaoOder(int accid, int cartid, Request_ThemOrder request);
        Task<string> XacNhanOrder(Request_ValidateRegister request);
        Task<ResponseObject<OrderDTOs>> SuaOder(int id , Request_SuaOrder request);
        Task<ResponseObject<OrderDTOs>> XoaOder(int id);
        Task<PageResult<OrderDTOs>> HienThiOrderBanThan(int id, Pagintation pagintation);
        Task<string> XoaOrderChuaDuyet();
        Task<PageResult<OrderDTOs>> getAll(Pagintation pagintation);
    }
}
