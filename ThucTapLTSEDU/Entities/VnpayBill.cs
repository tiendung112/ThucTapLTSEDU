namespace ThucTapLTSEDU.Entities
{
    public class VnpayBill :BaseEntity
    {   
        // Thông tin đơn hàng
        public int OrderId { get; set; }  // Mã đơn hàng trong hệ thống của bạn
        public Order? Order { get; set; }
        public double Amount { get; set; }   // Tổng số tiền thanh toán
        public string OrderInfo { get; set; } // Thông tin mô tả đơn hàng

        // Thông tin thanh toán VNPay
        public string VnPayTransactionId { get; set; } // Mã giao dịch của VNPay
        public bool IsPaid { get; set; }// Trạng thái thanh toán (đã thanh toán hay chưa)

        // Thời gian tạo đơn hàng và thời gian thanh toán
        public DateTime CreatedAt { get; set; }
        public DateTime? PaidAt { get; set; } // Thời gian thanh toán (nếu đã thanh toán)

    }
}
