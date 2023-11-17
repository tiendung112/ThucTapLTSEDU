using System.ComponentModel.DataAnnotations.Schema;

namespace ThucTapLTSEDU.Entities
{
    [Table(name: "product")]
    public class Product : BaseEntity
    {
        public int product_typeID { get; set; }    
        public Product_type? product { get; set; }
        public string? name_product { get; set; }
        public double? price { get; set; }
        public string? avartar_image_product { get; set; }

        public string? title { get; set; }
        public int discount { get; set; }
        public int status { get; set; }
        public int number_of_views { get; set; }
        public DateTime? created_at { get; set; } = DateTime.Now;
        public DateTime? updated_at { get; set; }
        public IEnumerable<Product_review>? reviews { get; set; }
        public IEnumerable<Cart_item>? cart_Items { get; set; }
        public IEnumerable<Order_detail>? order_Details { get; set; }


    }
}
