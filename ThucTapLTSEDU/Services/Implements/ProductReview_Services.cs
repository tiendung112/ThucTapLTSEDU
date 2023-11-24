using ThucTapLTSEDU.Entities;
using ThucTapLTSEDU.Pagination;
using ThucTapLTSEDU.Payloads.Responses;
using ThucTapLTSEDU.PayLoads.Converters;
using ThucTapLTSEDU.PayLoads.DTOs;
using ThucTapLTSEDU.PayLoads.Requests.Product_Review;
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
        public async Task<IQueryable<Product_reviewDTOs>> HienThiDSReview(int spid, Pagintation pagintation)
        {
            return spid == 0 ? context.Products_review.OrderBy(y => y.productID).Select(x => converters.EntityToDTOs(x))
                : context.Products_review.Where(z => z.productID == spid).Select(x => converters.EntityToDTOs(x));
        }

        public async Task<ResponseObject<Product_reviewDTOs>> SuaReview(int id, Request_SuaProductReview request)
        {
            var review =context.Products_review.SingleOrDefault(x=>x.Id==id);
            if(review == null)
            {
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Không tồn tại review này", null);
            }
            review.status = 1;
            review.updated_at = DateTime.Now;
            review.content_rated = request.content_rated;
            review.point_evaluation = request.point_evaluation;
            context.Products_review.Update(review);
            await context.SaveChangesAsync();
            return responseObject.ResponseSuccess("Sửa Review thành công ", converters.EntityToDTOs(review));
        }

        public async Task<ResponseObject<Product_reviewDTOs>> ThemReview(int id, Request_ThemProductReview request)
        {
            if (!context.Products.Any(x => x.Id == request.productID))
            {
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Không tồn tại sản phẩm này",null);
            }
            var user = context.Users.SingleOrDefault(x => x.accountID == id);
            Product_review newReview = new Product_review() {
                productID = request.productID,
                userID =user.Id,
                created_at = DateTime.Now,
                point_evaluation= request.point_evaluation,
                content_seen= request.content_seen,
                content_rated= request.content_rated,
                status=request.status,
            };
            await context.Products_review.AddAsync(newReview);
            await context.SaveChangesAsync();
            return responseObject.ResponseSuccess("Thêm review thành công ",converters.EntityToDTOs(newReview));
        }

        public async Task<ResponseObject<Product_reviewDTOs>> XoaReview(int id)
        {
            var review = context.Products_review.SingleOrDefault(x => x.Id == id);
            if (review == null)
            {
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Không tồn tại review này", null);
            }
            context.Remove(review);
            await context.SaveChangesAsync();
            return responseObject.ResponseSuccess("Xoá review thành công ", converters.EntityToDTOs(review));
        }
    }
}
