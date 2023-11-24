using ThucTapLTSEDU.Entities;

namespace ThucTapLTSEDU.PayLoads.DTOs
{
    public class Order_DetailDTOs
    {
        public int order_detailID {  get; set; }
        public int orderID { get; set; }
        public int? productID { get; set; }
        public Product? product { get; set; }
        public double? price_total { get; set; }
        public int? quantity { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
