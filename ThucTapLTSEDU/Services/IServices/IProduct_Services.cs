using ThucTapLTSEDU.Pagination;
using ThucTapLTSEDU.Payloads.Responses;
using ThucTapLTSEDU.PayLoads.DTOs;
using ThucTapLTSEDU.PayLoads.Requests.Products;

namespace ThucTapLTSEDU.Services.IServices
{
    public interface IProduct_Services
    {
        Task<PageResult<ProductDTOs>> HienThiDanhSachSanPham(Pagintation pagintation);
        Task<ResponseObject<ProductDTOs>> ThemSanPham(Request_ThemProduct request);
        Task<ResponseObject<ProductDTOs>> SuaSanPham(int id,Request_SuaProduct request);
        Task<ResponseObject<ProductDTOs>> XoaSanPham(int id );
    }
}
