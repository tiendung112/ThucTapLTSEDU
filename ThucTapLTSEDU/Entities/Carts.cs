using System.ComponentModel.DataAnnotations.Schema;

namespace ThucTapLTSEDU.Entities
{
    [Table(name:"Carts")]
    public class Carts :BaseEntity
    {
        public int userID { get; set; }
        public User? user { get; set; }
        public DateTime? created_at { get; set; } = DateTime.Now;
        public DateTime? updated_at { get; set; }
        public IEnumerable<Cart_item>? cart_Items { get; set; }

    }
}
