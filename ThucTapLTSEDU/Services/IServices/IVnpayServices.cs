namespace ThucTapLTSEDU.Services.IServices
{
    public interface IVnpayServices
    {

        Task<string> CreatePaymentUrl(int orderID, HttpContext httpContext);
        Task<string> VNPayReturn(IQueryCollection vnpayData);
    }
}
