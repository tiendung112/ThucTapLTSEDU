using System.ComponentModel.DataAnnotations.Schema;

namespace ThucTapLTSEDU.Entities
{
    [Table(name:"Cart_item")]
    public class Cart_item :BaseEntity
    {
        public int productId {  get; set; }
        public Product? product { get; set; }

        public int cartID { get; set; }
        public Carts? cart { get; set; }
        public int? quantity { get; set; }
        public DateTime? created_at { get; set; } = DateTime.Now;
        public DateTime? updated_at { get; set; }
    }
}
