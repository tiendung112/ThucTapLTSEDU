using System.Net.Mail;
using System.Net;
using ThucTapLTSEDU.Context;
using ThucTapLTSEDU.Handler.Email;
using SmtpClient = System.Net.Mail.SmtpClient;

namespace ThucTapLTSEDU.Services.Implements
{
    public class BaseServices
    {
        public readonly AppDBContext context;
        public BaseServices()
        {
            context = new AppDBContext();
        }
        public int GenerateCodeActive()
        {
            Random random = new Random();
            return random.Next(100000, 999999);
        }
        public string SendEmail(EmailTo emailTo)
        {
            if (!Validate.IsValidEmail(emailTo.Mail))
            {
                return "Định dạng email không hợp lệ";
            }
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("dung0112.dev.test@gmail.com", "xssyibnbzpqhmzsz"),
                EnableSsl = true
            };
            try
            {
                var message = new MailMessage();
                message.From = new MailAddress("dung0112.dev.test@gmail.com");
                message.To.Add(emailTo.Mail);
                message.Subject = emailTo.Subject;
                message.Body = emailTo.Content;
                message.IsBodyHtml = true;
                smtpClient.Send(message);

                return "Gửi email thành công";
            }
            catch (Exception ex)
            {
                return "Lỗi khi gửi email: " + ex.Message;
            }
        }
    }
}
