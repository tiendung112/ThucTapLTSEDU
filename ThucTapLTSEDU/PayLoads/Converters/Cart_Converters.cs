using ThucTapLTSEDU.Entities;
using ThucTapLTSEDU.PayLoads.DTOs;
using ThucTapLTSEDU.Services.Implements;

namespace ThucTapLTSEDU.PayLoads.Converters
{
    public class Cart_Converters :BaseServices
    {
        private Cart_Item_Converters converters = new Cart_Item_Converters();
        public CartDTOs EntityToDTOs(Carts carts)
        {
            return new CartDTOs
            {
                userID = carts.userID,
                cart_Items = context.Cart_Items.Select(x=>converters.EntityToDTOs(x)),
                created_at = carts.created_at,
                updated_at = carts.updated_at,
                CartId = carts.Id,
            };
        }
    }

}
