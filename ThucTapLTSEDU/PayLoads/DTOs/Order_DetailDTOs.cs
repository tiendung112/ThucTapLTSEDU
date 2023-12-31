﻿using ThucTapLTSEDU.Entities;

namespace ThucTapLTSEDU.PayLoads.DTOs
{
    public class Order_DetailDTOs
    {
        public int order_detailID {  get; set; }
        public string? productname{ get; set; }
        public double? price_total { get; set; }
        public int? quantity { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
