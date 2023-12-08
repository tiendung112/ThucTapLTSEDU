using System.Net.WebSockets;
using ThucTapLTSEDU.Entities;
using ThucTapLTSEDU.Services.Implements;

namespace ThucTapLTSEDU.Handler.Email
{
    public class BillOrder 
    {
        private static readonly Context.AppDBContext context = new Context.AppDBContext();
        public static string GenerateNotificationBillEmail(Order order, string message = "")
        {
            string htmlContent = $@"
            <html>
            <head>
                <style>
                    body {{
                        font-family: Arial, sans-serif;
                    }}
                    image {{
                        width: 60px;
                        height: 70px;
                    }}
                    h1 {{
                        color: #333;
                    }}
                    
                    table {{
                        border-collapse: collapse;
                        width: 100%;
                    }}
                    
                    th, td {{
                        border: 1px solid #ddd;
                        padding: 8px;
                    }}
                    
                    th {{
                        background-color: #f2f2f2;
                        font-weight: bold;
                    }}
                    
                    .footer {{
                        margin-top: 20px;
                        font-size: 14px;
                    }}
                </style>
            </head>
            <body>
                <h1>Thông tin hóa đơn đặt hangf</h1>
                <h2 style=""color: red; font-size: 20px; font-weight: bold;"">{(string.IsNullOrEmpty(message) ? "" : message)}</h2>
                ";
            htmlContent += @"<h2>Chi tiết Sản phẩm order</h2>
                <table>
                    <tr>
                        <th></th>
                        <th>Tên sản phẩm</th>
                        <th>Số lượng</th>
                        <th>Giá</th>
                    </tr>";
            int rowIndex = 1;
            var orderdetail = context.Order_Details.Where(x => x.orderID == order.Id).ToList();
            foreach(var item in orderdetail)
            {
                var sp = context.Products.SingleOrDefault(x => x.Id == item.productID);
                htmlContent += $@"
                    <tr>
                        <td>{rowIndex}</td>
                        <td>{sp.name_product}</td>
                        <td>{item.quantity}</td>
                        <td>{item.price_total}</td>
                    </tr>";
                rowIndex++;
            }

            htmlContent += $@"
                       <tr>
                        <td style=""text-align: center;"">Tổng tiền</td>
                        <td colspan=""3"" style=""text-align: right;"">{order.original_price}</td>
                    </tr>
                </table>
                
                <div class=""footer"">
                    <p>Trân trọng,</p>
                    <p></p>
                </div>
            </body>
            </html>";

            return htmlContent;
        }
    }
}
