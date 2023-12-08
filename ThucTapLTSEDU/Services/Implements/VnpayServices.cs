using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sek.Module.Payment.VnPay.Common;
using SixLabors.ImageSharp;
using ThucTapLTSEDU.Entities;
using ThucTapLTSEDU.Handler.Email;
using ThucTapLTSEDU.Handler.VNPay;
using ThucTapLTSEDU.Services.IServices;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace ThucTapLTSEDU.Services.Implements
{
    public class VnpayServices :BaseServices , IVnpayServices
    {
        private readonly IConfiguration _configuration;
        private readonly VNPayLibrary pay;
        public VnpayServices(IConfiguration configuration)
        {
            _configuration = configuration;
            pay = new VNPayLibrary();
        }
        public async Task<string> CreatePaymentUrl(int orderID,HttpContext httpContext,int id)
        {
            var order = context.Orders.SingleOrDefault(x=>x.Id==orderID);
            if(context.Users.SingleOrDefault(x=>x.accountID==id).Id==order.userID)
            {
                if (order.order_statusID == 1)
                {
                    return "Đon hàng của bạn chưa được duyệt ";
                }

                if (order.order_statusID == 3)
                {
                    return "Đon hàng của bạn đã được thanh toán ";
                }

                //pay.AddRequestData("vnp_BankCode", "VNPAYQR");
                pay.AddRequestData("vnp_Version", "2.1.0");
                pay.AddRequestData("vnp_Command", "pay");
                pay.AddRequestData("vnp_TmnCode", "YIK14C5R");
                pay.AddRequestData("vnp_Amount", (order.actual_price * 1000).ToString());
                pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
                pay.AddRequestData("vnp_CurrCode", "VND");
                pay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(httpContext));
                pay.AddRequestData("vnp_Locale", "vn");
                pay.AddRequestData("vnp_OrderInfo", $"Thanh toan don hang{orderID}");
                pay.AddRequestData("vnp_OrderType", "other");
                pay.AddRequestData("vnp_ReturnUrl", _configuration.GetSection("VnPay:vnp_ReturnUrl").Value);
                pay.AddRequestData("vnp_TxnRef", orderID.ToString());

                string paymentUrl = pay.CreateRequestUrl(_configuration.GetSection("VnPay:vnp_Url").Value, _configuration.GetSection("VnPay:vnp_HashSecret").Value);
                return paymentUrl;
            }
            return "xem lại đơn hàng của bạn";
            
        }
        public async Task<string> VNPayReturn(IQueryCollection vnpayData)
        {
                string vnp_TmnCode = _configuration.GetSection("VnPay:vnp_TmnCode").Value;
                string vnp_HashSecret = _configuration.GetSection("VnPay:vnp_HashSecret").Value;
                //var vnpayData = HttpContext.Request.Query;
                VNPayLibrary vnPayLibrary = new VNPayLibrary();
                foreach (var (key, value) in vnpayData)
                {
                    if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                    {
                        vnPayLibrary.AddResponseData(key, value);
                    }
                }
            string orderId = vnPayLibrary.GetResponseData("vnp_TxnRef");
                string vnp_ResponseCode = vnPayLibrary.GetResponseData("vnp_ResponseCode");
                string vnp_TransactionStatus = vnPayLibrary.GetResponseData("vnp_TransactionStatus");
                string vnp_SecureHash = vnPayLibrary.GetResponseData("vnp_SecureHash");
                double vnp_Amount = Convert.ToDouble(vnPayLibrary.GetResponseData("vnp_Amount"));
                bool check = vnPayLibrary.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
                if (check)
                {
                    if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                    {
                        var order = context.Orders.FirstOrDefault(x => x.Id == Convert.ToInt32(orderId));
                        order.order_statusID = 3;
                        order.updated_at = DateTime.Now;
                        context.Orders.Update(order);
                        context.SaveChanges();
                        string mss = SendEmail(new EmailTo
                        {
                            Mail = order.email,
                            Subject =$"Thanh Toán đơn hàng : {order.Id}" ,
                            Content = $"Bạn đã thanh toán đơn hàng : {order.Id} " +
                            $"\nVNPAY transaction = {vnp_TransactionStatus} \n" +
                            $"\nTên khách hàng {order.full_name}"+
                            $"\nEmail : {order.email}"+
                            $"\nĐịa Chỉ : {order.address}"+
                            $"\nGiá : {vnp_Amount} "
                            +$"\nThời gian thanh toán : {order.updated_at} "
                        });
                        return "Giao dịch thành công đơn hàng "+order.Id;
                    }
                    else
                    {
                        return $"Lỗi trong khi thực hiện giao dịch Mã lỗi : {vnp_ResponseCode}";
                    }

                }
                else
                {
                    return "có lỗi trong quá trình xử lí ";
                }
        }
      
    }
}
