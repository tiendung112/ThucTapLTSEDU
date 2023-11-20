using ThucTapLTSEDU.Context;
using ThucTapLTSEDU.Entities;
using ThucTapLTSEDU.PayLoads.DTOs;

namespace ThucTapLTSEDU.PayLoads.Converters
{
    public class Product_review_Converters
    {
        private readonly AppDBContext context;
        public Product_review_Converters()
        {
            context = new AppDBContext();
        }
        public Product_reviewDTOs EntityToDTOs(Product_review review)
        {
            return new Product_reviewDTOs() {
                Product_reviewID = review.Id,
                productID = review.productID,
                status = review.status,
                created_at = review.created_at,
                updated_at = review.updated_at,
                content_rated = review.content_rated,
                content_seen = review.content_seen,
                point_evaluation = review.point_evaluation,
                userName = context.Users.SingleOrDefault(x=>x.Id==review.userID).user_name,
            };
        }
    }
}
