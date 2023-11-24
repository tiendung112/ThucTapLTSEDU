using Microsoft.AspNetCore.Mvc.Razor.Infrastructure;
using ThucTapLTSEDU.Pagination;
using ThucTapLTSEDU.Payloads.Responses;
using ThucTapLTSEDU.PayLoads.DTOs;
using ThucTapLTSEDU.PayLoads.Requests.Orders;

namespace ThucTapLTSEDU.Services.IServices
{
    public interface IOrder_Services
    {
        Task<ResponseObject<OrderDTOs>> TaoOder(Request_ThemOrder request);
        Task<ResponseObject<OrderDTOs>> SuaOder(int id , Request_ThemOrder request);
        Task<ResponseObject<OrderDTOs>> XoaOder(int id);
        Task<PageResult<OrderDTOs>>HienThiDTOs(int id,Pagintation pagintation);

    }
}
