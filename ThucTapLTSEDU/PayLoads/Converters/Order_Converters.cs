using ThucTapLTSEDU.Entities;
using ThucTapLTSEDU.PayLoads.DTOs;

namespace ThucTapLTSEDU.PayLoads.Converters
{
    public class Order_Converters
    {
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
            };
        }
    }
}
