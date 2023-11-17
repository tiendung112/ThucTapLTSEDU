using System.ComponentModel.DataAnnotations.Schema;

namespace ThucTapLTSEDU.Entities
{
    [Table(name: "product_type")]
    public class Product_type : BaseEntity
    {

        public string? name_product_type { get; set; }
        public string? image_type_product { get; set; }
        public DateTime? created_at { get; set; } = DateTime.Now;
        public DateTime? updated_at { get; set; }

        public IEnumerable<Product>? products { get; set; }
    }
}
