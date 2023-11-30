using MailKit.Search;
using ThucTapLTSEDU.Entities;
using ThucTapLTSEDU.Payloads.Responses;
using ThucTapLTSEDU.PayLoads.Converters;
using ThucTapLTSEDU.PayLoads.DTOs;
using ThucTapLTSEDU.PayLoads.Requests.Cart;
using ThucTapLTSEDU.PayLoads.Requests.Cart_Item;
using ThucTapLTSEDU.Services.IServices;

namespace ThucTapLTSEDU.Services.Implements
{
    public class Cart_Services : BaseServices, ICart_Services
    {
        private readonly Cart_Converters converters;
        private readonly ResponseObject<CartDTOs> response;
        public Task<ResponseObject<CartDTOs>> SuaCart(int userid, Request_SuaCart request)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseObject<CartDTOs>> ThemCart(int userid, Request_ThemCart request)
        {
            var acc = context.Accounts.SingleOrDefault(x=>x.Id == userid);
            var user = context.Users.SingleOrDefault(x => x.accountID == acc.Id);
            Carts cart = new Carts() {
                created_at = DateTime.Now,
                userID = user.Id
            };
            context.Add(cart);
            await context.SaveChangesAsync();

            cart.cart_Items = themCartItem(cart.Id, request.cart_Items.ToList());
            context.Carts.Update(cart);
            await context.SaveChangesAsync();
            return response.ResponseSuccess("Thêm giỏi hàng thành côgn", converters.EntityToDTOs(cart));
        }
        private List<Cart_item> themCartItem(int cartid , List<Request_ThemCart_Items> cart_Item)
        {
            List<Cart_item> CartITem = new List<Cart_item>();
            foreach (var item in CartITem)
            {

                var sp = context.Products.SingleOrDefault(x => x.Id == item.productId);
                if (sp == null)
                {
                    throw new Exception("Sản phẩm không tồn tại");
                }
                Cart_item detail = new Cart_item()
                {
                   created_at= DateTime.Now,
                   productId = item.productId,
                   cartID = cartid,
                   quantity = item.quantity,
                };
                CartITem.Add(detail);
            }
            return CartITem;
        }

        public Task<ResponseObject<CartDTOs>> XoaCart(int userid, int id)
        {
            throw new NotImplementedException();
        }
    }
}
