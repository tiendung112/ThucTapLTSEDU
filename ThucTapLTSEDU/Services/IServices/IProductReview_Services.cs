using ThucTapLTSEDU.Entities;
using ThucTapLTSEDU.Pagination;
using ThucTapLTSEDU.Payloads.Responses;
using ThucTapLTSEDU.PayLoads.DTOs;
using ThucTapLTSEDU.PayLoads.Requests.Product_Review;

namespace ThucTapLTSEDU.Services.IServices
{
    public interface IProductReview_Services
    {
        Task<ResponseObject<Product_reviewDTOs>> ThemReview(int id, Request_ThemProductReview request);
        Task<ResponseObject<Product_reviewDTOs>> SuaReview(int id, Request_SuaProductReview request);
        Task<ResponseObject<Product_reviewDTOs>> XoaReview(int id);

        Task<IQueryable<Product_reviewDTOs>> HienThiDSReview(int spid , Pagintation pagintation);
    }
}
