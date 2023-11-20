using ThucTapLTSEDU.PayLoads.DTOs;

namespace ThucTapLTSEDU.PayLoads.Requests.Product_type
{
    public class Request_Update_Product_type
    {
        public int product_type_id { get; set; }
        public string? name_product_type { get; set; }
        public IFormFile? image_type_product { get; set; }
    }
}
