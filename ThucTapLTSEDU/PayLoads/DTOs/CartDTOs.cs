using ThucTapLTSEDU.Entities;

namespace ThucTapLTSEDU.PayLoads.DTOs
{
    public class CartDTOs
    {
        public int CartId { get; set; }
        public int userID { get; set; }
        public DateTime? created_at { get; set; } 
        public DateTime? updated_at { get; set; }
        public IQueryable<CartItemDTOs>? cart_Items { get; set; }
    }
}
