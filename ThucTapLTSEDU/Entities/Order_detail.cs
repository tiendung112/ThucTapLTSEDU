using System.ComponentModel.DataAnnotations.Schema;

namespace ThucTapLTSEDU.Entities
{
    [Table(name: "order_detail")]
    public class Order_detail : BaseEntity
    {
        public int orderID { get; set; }
        public Order? order { get; set; }
        public int? productID { get; set; }
        public Product? product { get; set; }
        public double? price_total { get; set; }
        public int? quantity { get; set; }
        public DateTime? created_at { get; set; } = DateTime.Now;
        public DateTime? updated_at { get; set; }
    }
}
