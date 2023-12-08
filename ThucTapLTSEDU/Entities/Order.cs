using System.ComponentModel.DataAnnotations.Schema;

namespace ThucTapLTSEDU.Entities
{
    [Table(name:"order")]
    public class Order : BaseEntity
    {
        public int paymentID { get; set; }
        public Payment? Payment { get; set; }
        public int userID { get; set; }
        public User? User { get; set; } 
        public double? original_price { get; set; }
        public double? actual_price { get; set; }
        
        public string? full_name { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public string? address { get; set; }
        public int order_statusID { get; set; }
        public Order_status?  order_Status {  get; set; }
        public DateTime? created_at { get; set; } = DateTime.Now;
        public DateTime? updated_at { get; set; }
        public IEnumerable<Order_detail>? order_Details { get; set; }
        public IEnumerable<VnpayBill>? vnpays { get; set; }
    }
}
