﻿namespace ThucTapLTSEDU.PayLoads.Requests.Product_Review
{
    public class Request_ThemProductReview
    {
        public int productID { get; set; }
   
        public string? content_rated { get; set; }
        public int point_evaluation { get; set; }
        public string? content_seen { get; set; }
        public int? status { get; set; }
    }
}
