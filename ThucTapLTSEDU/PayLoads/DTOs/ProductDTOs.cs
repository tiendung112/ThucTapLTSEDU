using ThucTapLTSEDU.Entities;

namespace ThucTapLTSEDU.PayLoads.DTOs
{
    public class ProductDTOs
    {
        public int Product_ID { get; set; }
        public int product_typeID { get; set; }
        public string? name_product { get; set; }
        public double? price { get; set; }
        public string? avartar_image_product { get; set; }
        public string? title { get; set; }
        public int discount { get; set; }
        public int status { get; set; }
        public int number_of_views { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }

    }
}
