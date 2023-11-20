using ThucTapLTSEDU.Entities;

namespace ThucTapLTSEDU.PayLoads.DTOs
{
    public class Product_reviewDTOs
    {
        public int Product_reviewID { get; set; }
        public int productID { get; set; }
        //public int userID { get; set; }
        public string userName {  get; set; }
        public string? content_rated { get; set; }
        public int point_evaluation { get; set; } = 0;
        public string? content_seen { get; set; }
        public int? status { get; set; }
        public DateTime? created_at { get; set; } 
        public DateTime? updated_at { get; set; }
    }
}
