using ThucTapLTSEDU.Entities;
using ThucTapLTSEDU.PayLoads.DTOs;

namespace ThucTapLTSEDU.PayLoads.Converters
{
    public class Oder_Detail_Converters
    {
        public Order_DetailDTOs EntityToDTOs(Order_detail entity)
        {
            return new Order_DetailDTOs()
            {
                orderID = entity.orderID,
                created_at = entity.created_at,
                order_detailID = entity.Id,
                updated_at = entity.updated_at,
                price_total = entity.price_total,
                productID = entity.productID,
                quantity = entity.quantity,
            };
        }
    }
}
