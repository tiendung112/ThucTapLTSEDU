using ThucTapLTSEDU.Entities;
using ThucTapLTSEDU.Pagination;
using ThucTapLTSEDU.PayLoads.DTOs;

namespace ThucTapLTSEDU.Services.IServices
{
    public interface IProductReview_Services
    {
        Task<IQueryable<Product_reviewDTOs>> HienThiDSReview(Pagintation pagintation);
    }
}
