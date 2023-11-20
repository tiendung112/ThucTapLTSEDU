using ThucTapLTSEDU.Pagination;
using ThucTapLTSEDU.Payloads.Responses;
using ThucTapLTSEDU.PayLoads.Converters;
using ThucTapLTSEDU.PayLoads.DTOs;
using ThucTapLTSEDU.Services.IServices;

namespace ThucTapLTSEDU.Services.Implements
{
    public class ProductReview_Services : BaseServices, IProductReview_Services
    {
        private readonly Product_review_Converters converters;
        private readonly ResponseObject<Product_reviewDTOs> responseObject;
        public ProductReview_Services()
        {
            converters = new Product_review_Converters();
            responseObject = new ResponseObject<Product_reviewDTOs>();
        }
        public async Task<IQueryable<Product_reviewDTOs>> HienThiDSReview(Pagintation pagintation)
        {
            return context.Products_review.OrderBy(y=>y.productID).Select(x => converters.EntityToDTOs(x));
        }
    }
}
