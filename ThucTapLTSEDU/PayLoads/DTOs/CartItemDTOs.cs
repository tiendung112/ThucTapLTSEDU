using ThucTapLTSEDU.Entities;

namespace ThucTapLTSEDU.PayLoads.DTOs
{
    public class CartItemDTOs
    {
        public int cardItemId {  get; set; }
        public int cartID { get; set; }
        public int productId { get; set; }
        public string productName { get; set; } 
        public int? quantity { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
