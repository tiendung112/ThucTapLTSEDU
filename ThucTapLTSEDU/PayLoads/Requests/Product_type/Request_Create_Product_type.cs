using ThucTapLTSEDU.PayLoads.DTOs;

namespace ThucTapLTSEDU.PayLoads.Requests.Product_type
{
    public class Request_Create_Product_type
    {
        public string? name_product_type { get; set; }
        public IFormFile? image_type_product { get; set; }
    }
}
