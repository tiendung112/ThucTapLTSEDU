using ThucTapLTSEDU.Context;
using ThucTapLTSEDU.Entities;
using ThucTapLTSEDU.PayLoads.DTOs;

namespace ThucTapLTSEDU.PayLoads.Converters
{
    public class Order_Converters
    {
        private readonly AppDBContext context;
        private readonly Oder_Detail_Converters converters;
        public Order_Converters()
        {
            context = new AppDBContext();
            converters = new Oder_Detail_Converters();
        }
        public OrderDTOs EntityToDTOs(Order order)
        {

            return new OrderDTOs()
            {
                OrderID = order.Id,
                full_name = order.full_name,
                address = order.address,
                created_at = order.created_at,
                updated_at = order.updated_at,
                email = order.email,
                order_statusID = order.order_statusID,
                paymentID = order.paymentID,
                original_price = order.original_price,
                phone = order.phone,
                userID = order.userID,
                actual_price = order.actual_price,
                Details = context.Order_Details.Where(x => x.orderID == order.Id).Select(y=>converters.EntityToDTOs(y)),
            };
        }
    }
}
