using Azure.Core;
using ThucTapLTSEDU.Pagination;
using ThucTapLTSEDU.Payloads.Responses;
using ThucTapLTSEDU.PayLoads.DTOs;
using ThucTapLTSEDU.PayLoads.Requests.Product_type;

namespace ThucTapLTSEDU.Services.IServices
{
    public interface IProductType_Services
    {
        Task<ResponseObject<Product_TypeDTOs>> CreateProudct_type(Request_Create_Product_type  request);
        Task<ResponseObject<Product_TypeDTOs>> UpdateProudct_type(Request_Update_Product_type request);
        Task<ResponseObject<Product_TypeDTOs>> DeleteProudct_type(int id );
        Task<IQueryable<Product_TypeDTOs>> DisplayProudct_type(Pagintation pagintation);

    }
}
