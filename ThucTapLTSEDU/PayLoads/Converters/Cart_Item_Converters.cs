using ThucTapLTSEDU.Entities;
using ThucTapLTSEDU.PayLoads.DTOs;
using ThucTapLTSEDU.Services.Implements;

namespace ThucTapLTSEDU.PayLoads.Converters
{
    public class Cart_Item_Converters :BaseServices
    {
        public CartItemDTOs EntityToDTOs(Cart_item cart_Item)
        {
            return new CartItemDTOs()
            {
                cardItemId = cart_Item.Id,
                cartID = cart_Item.cartID,
                created_at = cart_Item.created_at,
                updated_at = cart_Item.updated_at,
                productId = cart_Item.productId,
                productName = context.Products.SingleOrDefault(x=>x.Id==cart_Item.productId).name_product,
                quantity = cart_Item.quantity,
            };
        }
    }
}
