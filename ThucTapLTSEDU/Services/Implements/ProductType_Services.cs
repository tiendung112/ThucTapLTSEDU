using Azure;
using CloudinaryDotNet;
using ThucTapLTSEDU.Entities;
using ThucTapLTSEDU.Handler.Image;
using ThucTapLTSEDU.Pagination;
using ThucTapLTSEDU.Payloads.Responses;
using ThucTapLTSEDU.PayLoads.Converters;
using ThucTapLTSEDU.PayLoads.DTOs;
using ThucTapLTSEDU.PayLoads.Requests.Product_type;
using ThucTapLTSEDU.Services.IServices;

namespace ThucTapLTSEDU.Services.Implements
{
    public class ProductType_Services : BaseServices, IProductType_Services
    {
        private readonly ResponseObject<Product_TypeDTOs> responseObject;
        private readonly Product_Type_Converters converters;
        public ProductType_Services()
        {
            responseObject = new ResponseObject<Product_TypeDTOs>();
            converters = new Product_Type_Converters();
        }
        public async Task<ResponseObject<Product_TypeDTOs>> CreateProudct_type(Request_Create_Product_type request)
        {
            if (request.name_product_type == null)
            {
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Chưa điền đủ thông tin", null);

            }
            Product_type lsp = new Product_type()
            {
                name_product_type = request.name_product_type,
            };
            int imageSize = 2 * 2400 * 2400;
            if(request.image_type_product != null)
            {
                if (!HandleImage.IsImage(request.image_type_product, imageSize))
                {
                    return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Ảnh không hợp lệ", null);
                }
                else
                {
                    var avatarFile = await HandleUploadImage.Upfile(request.image_type_product,"Product_type/LoaisanPham");
                    lsp.image_type_product = avatarFile == "" ? "https://media.istockphoto.com/id/1300845620/vector/user-icon-flat-isolated-on-white-background-user-symbol-vector-illustration.jpg?s=612x612&w=0&k=20&c=yBeyba0hUkh14_jgv1OKqIH0CCSWU_4ckRkAoy2p73o=" : avatarFile;
                }
            }
            await context.AddAsync(lsp);
            await context.SaveChangesAsync();   
            return responseObject.ResponseSuccess("Thêm loại sản phẩm thành công ", converters.EntityToDTOs(lsp));
        }

        public async Task<ResponseObject<Product_TypeDTOs>> DeleteProudct_type(int id  )
        {
            var lsp = context.Products_type.SingleOrDefault(x => x.Id == id);
            if (lsp == null)
            {
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Không tồn tại loại sản phẩm này", null);
            }
            //xoá loại sản phẩm cần xoá sản phẩm
            var sp = context.Products.Where(x => x.product_typeID == id).ToList();
            foreach(var item in sp)
            {
                var cartItem = context.Cart_Items.Where(x => x.productId == item.Id); 
                context.RemoveRange(cartItem);
                var oderDetail = context.Order_Details.Where(x => x.productID == item.Id); 
                context.RemoveRange(oderDetail);
                var productReview = context.Products_review.Where(x=>x.productID == item.Id);
                context.RemoveRange(productReview);
            }
            context.RemoveRange(sp);
            context.Remove(lsp);
            await context.SaveChangesAsync();
            return responseObject.ResponseSuccess("Xoá loại sản phẩm thành công ", converters.EntityToDTOs(lsp));
        }

        public async Task<IQueryable<Product_TypeDTOs>> DisplayProudct_type(Pagintation pagintation)
        {
            return context.Products_type.Select(x => converters.EntityToDTOs(x));
        }

        public async Task<ResponseObject<Product_TypeDTOs>> UpdateProudct_type(Request_Update_Product_type request)
        {
            if (string.IsNullOrWhiteSpace(request.name_product_type))
            {
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Chưa điền đủ thông tin", null);
            }
            var lsp = context.Products_type.SingleOrDefault(x => x.Id == request.product_type_id);
            if (lsp == null)
            {
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Không tồn tại loại sản phẩm này", null);
            }
            int imageSize = 2 * 2400 * 2400;

            if (!HandleImage.IsImage(request.image_type_product, imageSize))
            {
                return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Ảnh không hợp lệ", null);
            }
            else
            {
                var avatarFile = await HandleUploadImage.Upfile(request.image_type_product, "Product_type");
                lsp.image_type_product = avatarFile == "" ? lsp.image_type_product: avatarFile;
            } 
            lsp.name_product_type = request.name_product_type;
            lsp.updated_at = DateTime.Now;
            context.Products_type.Update(lsp);
            await context.SaveChangesAsync();
            return responseObject.ResponseSuccess("Sửa loại sản phẩm thành công ", converters.EntityToDTOs(lsp));
        }
    }
}
