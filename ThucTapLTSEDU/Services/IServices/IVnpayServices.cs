namespace ThucTapLTSEDU.Services.IServices
{
    public interface IVnpayServices
    {

        Task<string> CreatePaymentUrl(int orderID, HttpContext httpContext,int id);
        Task<string> VNPayReturn(IQueryCollection vnpayData);
    }
}
