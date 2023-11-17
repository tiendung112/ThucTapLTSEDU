using ThucTapLTSEDU.Entities;

namespace ThucTapLTSEDU.PayLoads.DTOs
{
    public class UserDTOs
    {
        public string? user_name { get; set; }
        public int? accountID { get; set; }
        public string? phone { get; set; }
        public string? email { get; set; }
        public string? address { get; set; }
    }
}
