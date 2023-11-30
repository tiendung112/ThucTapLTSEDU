using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ThucTapLTSEDU.Entities;
using ThucTapLTSEDU.Handler.Email;
using ThucTapLTSEDU.Pagination;
using ThucTapLTSEDU.Payloads.Responses;
using ThucTapLTSEDU.PayLoads.Converters;
using ThucTapLTSEDU.PayLoads.DTOs;
using ThucTapLTSEDU.PayLoads.Requests.Auth;
using ThucTapLTSEDU.PayLoads.Requests.OrderDetails;
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

    
        public async Task<ResponseObject<OrderDTOs>> TaoOder(int accid, Request_ThemOrder request)
        {
            
            if (!context.Payment.Any(x => x.Id == request.paymentID))
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "Không tồn tại phương thức thanh toán này ", null);
            }
            var user = context.Users.SingleOrDefault(x => x.accountID == accid);
            Order order = new Order()
            {
                created_at = DateTime.Now,
                address = request.address,
                full_name = user.user_name,
                email = user.email,
                order_statusID = 1,
                userID = user.Id,
                paymentID = request.paymentID,
                phone = request.phone,
            };
            context.Orders.Add(order);
            await context.SaveChangesAsync();

            List<Order_detail> list =await CreateOrderDetail(order.Id,request.details);
            order.order_Details = list;
            order.actual_price = list.Sum(x => x.price_total);
            order.original_price = 0;
            foreach (var item in list)
            {
                var sp = context.Products.SingleOrDefault(x => x.Id == item.productID);
                order.original_price += sp.price * item.quantity;
            }
            context.Orders.Update(order);
            await context.SaveChangesAsync();

            //xoá hết kích hoạt liên quan đến tài khoản
            var confrim = context.EmailValidates.Where(x => x.AccountID == accid);
            context.EmailValidates.RemoveRange(confrim);
            await context.SaveChangesAsync();

            // gửi mail kích hoạt tài khoản
            EmailValidate confrimEmail = new EmailValidate()
            {
                AccountID =accid,
                DaXacNhan = false,
                ThoiGianHetHan = DateTime.Now.AddMinutes(15),
                MaXacNhan = GenerateCodeActive().ToString(),
            };
            await context.EmailValidates.AddAsync(confrimEmail);
            await context.SaveChangesAsync();

            string mss = SendEmail(new EmailTo
            {
                Mail = order.email,
                Subject = "Nhận mã xác nhận để tạo mật khẩu mới từ đây: ",
                Content = $"Mã kích hoạt của bạn là: {confrimEmail.MaXacNhan}, mã này sẽ hết hạn sau 15 phút"
            });
            return response.ResponseSuccess
                ("Bạn đã gửi yêu cầu order ," +
                " vui lòng nhập mã xác nhận đã được gửi về email của bạn"
                , converters.EntityToDTOs(order));
        }
        private async Task<List<Order_detail>> CreateOrderDetail(int orderId, List<Request_ThemOrderDetail> order_Details)
        {
            List<Order_detail> details = new List<Order_detail>();
            foreach(var item in order_Details)
            {

                var sp = context.Products.SingleOrDefault(x=>x.Id == item.productID);
                if (sp == null)
                {
                    throw new Exception("Sản phẩm không tồn tại");
                }
                Order_detail detail = new Order_detail()
                {
                    created_at = DateTime.Now,
                    orderID = orderId,
                    quantity = item.quantity,
                    price_total = item.quantity*sp.price*sp.discount /100,
                    productID = item.productID,
                };
                details.Add(detail);
            }
            return details;
        }

        public async Task<string> XacNhanOrder(Request_ValidateRegister request)
        {
            EmailValidate confirmEmail = await context.EmailValidates
                .Where(x => x.MaXacNhan.Equals(request.MaXacNhan))
                .FirstOrDefaultAsync();
            if (confirmEmail is null)
            {
                return "Mã xác nhận không chính xác";
            }
            if (confirmEmail.ThoiGianHetHan < DateTime.Now)
            {
                return "Mã xác nhận đã hết hạn";
            }
            Order order = context.Orders.FirstOrDefault(x => x.Id == confirmEmail.AccountID);
            order.order_statusID = 4;
            order.updated_at = DateTime.Now;
            context.EmailValidates.Remove(confirmEmail);
            context.Orders.Update(order);
            await context.SaveChangesAsync();
            return "Xác nhận order thành công";
        }

        public async Task<ResponseObject<OrderDTOs>> XoaOder(int id)
        {
            var order = context.Orders.SingleOrDefault(x => x.Id == id);
            if(order == null)
            {

                return response.ResponseError(StatusCodes.Status404NotFound, "Không tồn tại order này", null);
            }
            var lstorderDetail = context.Order_Details.Where(x => x.orderID == order.Id);
            context.RemoveRange(lstorderDetail);
            context.Remove(order);
            context.SaveChanges();
            return response.ResponseSuccess("xoá order thành công", converters.EntityToDTOs(order));
        }

        public async Task<ResponseObject<OrderDTOs>> SuaOder(int id, Request_SuaOrder request)
        {
            var order = context.Orders.SingleOrDefault(x => x.Id == id);
            if (order == null)
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "Không tồn tại order này", null);
            }
            order.updated_at = DateTime.UtcNow;
            order.phone = request.phone;
            order.address = request.address;
            return response.ResponseSuccess("Sửa order thành công", converters.EntityToDTOs(order));
        }

        public async Task<string> XoaOrderChuaDuyet()
        {
            var order = context.Orders.Where(x => x.order_statusID ==1).ToList();

            foreach (var t in order)
            {
                var lstorderDetail = context.Order_Details.Where(x => x.orderID == t.Id);
                context.RemoveRange(lstorderDetail);
                context.SaveChanges();
                DateTime? next5day = t.created_at + TimeSpan.FromDays(5);
                if (t.created_at < DateTime.Now)
                {
                    context.Remove(t);
                }
            }
            await context.SaveChangesAsync();
            return "đã xoá hết đơn hàng chưa kích hoạt";
        }

        public async Task<PageResult<OrderDTOs>> getAll(Pagintation pagintation)
        {
            var sp = context.Orders.Select(x => converters.EntityToDTOs(x));
            var page = PageResult<OrderDTOs>.toPageResult(pagintation, sp);
            PageResult<OrderDTOs> result = new PageResult<OrderDTOs>(pagintation,page);
            return result;
        }
    }
}
