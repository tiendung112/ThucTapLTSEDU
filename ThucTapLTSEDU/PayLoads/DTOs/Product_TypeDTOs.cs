namespace ThucTapLTSEDU.PayLoads.DTOs
{
    public class Product_TypeDTOs
    {
        public int product_type_id {  get; set; }
        public string? name_product_type { get; set; }
        public string? image_type_product { get; set; }
        public DateTime? created_at { get; set; } = DateTime.Now;
        public DateTime? updated_at { get; set; }
        public IEnumerable<ProductDTOs>? ProductDTOs { get; set; }
    }
}
