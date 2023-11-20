using ThucTapLTSEDU.Pagination;
using ThucTapLTSEDU.PayLoads.Converters;
using ThucTapLTSEDU.PayLoads.DTOs;
using ThucTapLTSEDU.Services.IServices;

namespace ThucTapLTSEDU.Services.Implements
{
    public class Product_Services : BaseServices, IProduct_Services
    {
        private readonly Product_Converters converters;
        public Product_Services()
        {
            converters = new Product_Converters();
        }
        public async Task<IQueryable<ProductDTOs>> HienThiDanhSachSanPham(Pagintation pagintation)
        {
            return context.Products.Select(x => converters.EntityToDTOs(x));
        }
    }
}
