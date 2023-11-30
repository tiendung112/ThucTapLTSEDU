using ThucTapLTSEDU.Context;
using ThucTapLTSEDU.Entities;
using ThucTapLTSEDU.PayLoads.DTOs;

namespace ThucTapLTSEDU.PayLoads.Converters
{
    public class Oder_Detail_Converters
    {
        private readonly AppDBContext context;
        public Oder_Detail_Converters()
        {
            context = new AppDBContext();
        }
        public Order_DetailDTOs EntityToDTOs(Order_detail entity)
        {
            return new Order_DetailDTOs()
            {
                created_at = entity.created_at,
                order_detailID = entity.Id,
                updated_at = entity.updated_at,
                price_total = entity.price_total,
                productname = context.Products.SingleOrDefault(x=>x.Id==entity.productID).name_product,
                quantity = entity.quantity,
            };
        }
    }
}
