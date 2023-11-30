using Azure;
using ThucTapLTSEDU.Entities;
using ThucTapLTSEDU.Handler.Image;
using ThucTapLTSEDU.Pagination;
using ThucTapLTSEDU.Payloads.Responses;
using ThucTapLTSEDU.PayLoads.Converters;
using ThucTapLTSEDU.PayLoads.DTOs;
using ThucTapLTSEDU.PayLoads.Requests.Products;
using ThucTapLTSEDU.Services.IServices;

namespace ThucTapLTSEDU.Services.Implements
{
    public class Product_Services : BaseServices, IProduct_Services
    {
        private readonly Product_Converters converters;
        private readonly ResponseObject<ProductDTOs> response;
        public Product_Services()
        {
            converters = new Product_Converters();
        }

        public async Task<PageResult<ProductDTOs>> HienThiCacSanPhamNoiBat()
        {
            Pagintation pagintation = new Pagintation() { PageNumber = 0, PageSize = 15};

            var sp = context.Products.OrderBy(y=>y.number_of_views).Select(x => converters.EntityToDTOs(x));
            var res = PageResult<ProductDTOs>.toPageResult(pagintation, sp);
            PageResult<ProductDTOs> result = new PageResult<ProductDTOs>(pagintation, res);
            return result;
        }
        public async Task<ProductDTOs> HienThiSanPham(int id)
        {
            var product = context.Products.SingleOrDefault(x => x.Id == id);
            product.number_of_views++;
            context.Update(product);
            await context.SaveChangesAsync();
            var result = converters.EntityToDTOs(product);
            return result;
        }

        public async Task<PageResult<ProductDTOs>> HienThiDanhSachSanPham(Pagintation pagintation)
        {
            var sp = context.Products.Select(x => converters.EntityToDTOs(x));
            var res =PageResult<ProductDTOs>.toPageResult(pagintation, sp);
            PageResult<ProductDTOs> result = new PageResult<ProductDTOs>(pagintation,res);
            return result;
        }

        public async Task<ResponseObject<ProductDTOs>> SuaSanPham(int id, Request_SuaProduct request)
        {
            var sp = context.Products.SingleOrDefault(x => x.Id == id);
            if(sp == null)
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "không tồn tại sản phẩm này", null);
            }
            if(request.product_typeID !=0&& !context.Products_type.Any(x => x.Id == request.product_typeID)){
                return response.ResponseError(StatusCodes.Status404NotFound, "không tồn tại loại sản phẩm này", null);
            }
            sp.name_product = request.name_product==null?sp.name_product:request.name_product;
            sp.price = request.price==0?sp.price:request.price;
            sp.title=request.title==null?sp.title:request.title;
            sp.status =request.status;
            sp.created_at = DateTime.Now;
            sp.discount = request.discount;
            int imageSize = 2 * 2048 * 1024;
            if (!HandleImage.IsImage(request.avartar_image_product, imageSize))
            {
                return response.ResponseError(StatusCodes.Status400BadRequest, "Ảnh không hợp lệ", null);
            }
            else
            {
                var anhSP = await HandleUploadImage.Upfile(request.avartar_image_product, $"/TTLTSEDU/Product/{sp.Id}");
                sp.avartar_image_product = anhSP == "" ? sp.avartar_image_product  : anhSP;
            }
            context.Products.Update(sp);
            await context.SaveChangesAsync();
            return response.ResponseSuccess("Sửa thành công sản phẩm ", converters.EntityToDTOs(sp));
        }

        public async Task<ResponseObject<ProductDTOs>> ThemSanPham(Request_ThemProduct request)
        {
            if (!context.Products_type.Any(x => x.Id == request.product_typeID))
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "không tồn tại loại sản phẩm này", null);
            }
            if(string.IsNullOrWhiteSpace(request.name_product) || string.IsNullOrWhiteSpace(request.title)||request.price==0)
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "Chưa điền đủ thông tin", null);
            }
            int imageSize = 2 * 2048 * 1024;
            try
            {
                
                Product sp = new Product();
                sp.name_product = request.name_product;
                sp.title = request.title;
                sp.price = request.price;
                sp.product_typeID = request.product_typeID;
                sp.created_at = DateTime.Now;
                sp.number_of_views = 0;

                sp.status = 1;
                
                if (request.avartar_image_product != null)
                {
                    if (!HandleImage.IsImage(request.avartar_image_product, imageSize))
                    {
                        return response.ResponseError(StatusCodes.Status400BadRequest, "Ảnh không hợp lệ", null);
                    }
                    else
                    {
                        var anhSP = await HandleUploadImage.Upfile(request.avartar_image_product, $"/TTLTSEDU/Product/{sp.Id}");
                        sp.avartar_image_product = anhSP == "" ? $"ảnh của sản phẩm {sp.name_product}" : anhSP;
                    }
                }
                return response.ResponseSuccess("thêm thành công sản phẩm ",converters.EntityToDTOs(sp));

            }
            catch (Exception e)
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "Lỗi " + e, null);
            }
        }

        public async Task<ResponseObject<ProductDTOs>> XoaSanPham(int id)
        {
            var sp = context.Products.SingleOrDefault(x => x.Id == id);
            if (sp == null)
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "không tồn tại sản phẩm này", null);
            }
            var lstReview = context.Products_review.Where(x => x.productID == id);
            context.RemoveRange(lstReview);

            var lstORderDetail = context.Order_Details.Where(x=>x.productID == id);
            context.RemoveRange(lstORderDetail);

            var lstCartItem  =context.Cart_Items.Where(x=>x.productId == id);
            context.RemoveRange(lstCartItem);

            context.Remove(sp);
            await context.SaveChangesAsync();
            return response.ResponseSuccess("Xoá thành công sản phẩm ", converters.EntityToDTOs(sp));
        }
    }
}
