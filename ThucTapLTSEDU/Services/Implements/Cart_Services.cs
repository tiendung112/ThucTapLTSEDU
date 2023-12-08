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
        public Cart_Services()
        {
            converters = new Cart_Converters();
            response = new ResponseObject<CartDTOs>();
        }
        public async Task<IQueryable<CartDTOs>> HienThiCartTheoID(int accid)
        {
            var user = context.Users.SingleOrDefault(x => x.accountID == accid);
            return context.Carts.Where(x => x.userID == user.Id).Select(x => converters.EntityToDTOs(x));
        }
        public async Task<ResponseObject<CartDTOs>> SuaCart(int userid,int cartid, Request_ThemCart request)
        {
            var cart = context.Carts.SingleOrDefault(x=>x.Id == cartid);
            var user = context.Users.SingleOrDefault(x=>x.accountID==userid);
            if(cart == null && cart.userID!=user.Id) 
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "không tồn tại giỏ hàng này ", null);
            }
            var cartItem = context.Cart_Items.Where(x => x.cartID == cartid);
            if (cartItem != null)
            {
                context.RemoveRange(cartItem);
                cart.cart_Items = themCartItem(cartid, request.cart_Items.ToList());
                context.Carts.Update(cart);
            }
            await context.SaveChangesAsync();
            return response.ResponseSuccess("sửa giỏ hàng thành công ",converters.EntityToDTOs(cart));
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
            return response.ResponseSuccess("Thêm giỏ hàng thành công", converters.EntityToDTOs(cart));
        }
        private List<Cart_item> themCartItem(int cartid , List<Request_ThemCart_Items> cart_Item)
        {
            List<Cart_item> CartITem = new List<Cart_item>();
            foreach (var item in cart_Item)
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

        public async Task<ResponseObject<CartDTOs>> XoaCart(int userid, int id)
        {
            var cart = context.Carts.SingleOrDefault(x => x.Id == id);
            if (cart == null && cart.userID != userid)
            {
                return response.ResponseError(StatusCodes.Status404NotFound,"Không tồn tại giỏ hàng này",null);
            }
            else
            {
                var lstCartItem = context.Cart_Items.Where(x => x.cartID == cart.Id);
                context.RemoveRange(lstCartItem);
                context.Remove(cart);
                await context.SaveChangesAsync();
                return response.ResponseSuccess("xoá giỏ hàng thành công ",converters.EntityToDTOs(cart));
            }
        }
    }
}
