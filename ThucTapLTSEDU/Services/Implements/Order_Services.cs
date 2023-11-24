using ThucTapLTSEDU.Pagination;
using ThucTapLTSEDU.Payloads.Responses;
using ThucTapLTSEDU.PayLoads.Converters;
using ThucTapLTSEDU.PayLoads.DTOs;
using ThucTapLTSEDU.PayLoads.Requests.Orders;
using ThucTapLTSEDU.Services.IServices;

namespace ThucTapLTSEDU.Services.Implements
{
    public class Order_Services : BaseServices, IOrder_Services
    {
        private readonly Order_Converters converters;
        private readonly ResponseObject<OrderDTOs> response;
        public Order_Services()
        {
            converters = new Order_Converters();
            response = new ResponseObject<OrderDTOs>();
        }
        public async Task<PageResult<OrderDTOs>> HienThiDTOs(int id,Pagintation pagintation)
        {
            var order = id == 0? context.Orders.Select(x => converters.EntityToDTOs(x)): context.Orders.Where(y=>y.Id==id).Select(x => converters.EntityToDTOs(x));
            var result = PageResult<OrderDTOs>.toPageResult(pagintation, order);
            return  new PageResult<OrderDTOs>(pagintation, result);
        }

        public Task<ResponseObject<OrderDTOs>> SuaOder(int id, Request_ThemOrder request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseObject<OrderDTOs>> TaoOder(Request_ThemOrder request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseObject<OrderDTOs>> XoaOder(int id)
        {
            throw new NotImplementedException();
        }
    }
}
