using ThucTapLTSEDU.Pagination;
using ThucTapLTSEDU.PayLoads.DTOs;

namespace ThucTapLTSEDU.Services.IServices
{
    public interface IProduct_Services
    {
        Task<IQueryable<ProductDTOs>> HienThiDanhSachSanPham(Pagintation pagintation);

    }
}
