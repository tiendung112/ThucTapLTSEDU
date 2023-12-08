using ThucTapLTSEDU.Payloads.Responses;
using ThucTapLTSEDU.PayLoads.DTOs;
using ThucTapLTSEDU.PayLoads.Requests.Cart;

namespace ThucTapLTSEDU.Services.IServices
{
    public interface ICart_Services
    {
        Task<ResponseObject<CartDTOs>> ThemCart(int userid, Request_ThemCart request);
        Task<ResponseObject<CartDTOs>> SuaCart(int userid, int cartid, Request_ThemCart request);
        Task<ResponseObject<CartDTOs>> XoaCart(int userid, int id);
        Task<IQueryable<CartDTOs>> HienThiCartTheoID(int accid);
    }
}
