﻿namespace ThucTapLTSEDU.PayLoads.Requests.Orders
{
    public class Request_SuaOrder
    {
        public int? OrderID { get; set; }
        public int? paymentID { get; set; }
        public int? userID { get; set; }
        public double? original_price { get; set; }
        public double? actual_price { get; set; }

        public string? full_name { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public string? address { get; set; }
        public int? order_statusID { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}