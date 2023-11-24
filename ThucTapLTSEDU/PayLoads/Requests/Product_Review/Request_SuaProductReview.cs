namespace ThucTapLTSEDU.PayLoads.Requests.Product_Review
{
    public class Request_SuaProductReview
    {
        //public int Product_reviewID { get; set; }
        //public int productID { get; set; }
        //public int userID { get; set; }
        public string? content_rated { get; set; }
        public int point_evaluation { get; set; }
        public string? content_seen { get; set; }
    }
}
