using ThucTapLTSEDU.Entities;

namespace ThucTapLTSEDU.PayLoads.DTOs
{
    public class AccountDTOs
    {
        public string? user_name { get; set; }
        public string? avatar { get; set; }
        public string? password { get; set; }
        public int? status { get; set; } = 1;
        public IEnumerable<UserDTOs>? users { get; set; }
    }
}
