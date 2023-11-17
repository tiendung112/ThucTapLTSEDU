using System.ComponentModel.DataAnnotations.Schema;

namespace ThucTapLTSEDU.Entities
{
    [Table(name: "Refresh_token")]
    public class RefreshToken :BaseEntity
    {
        public string? Token { get; set; }
        public DateTime? ExpiredTime { get; set; }
        public int? accountID { get; set; }
        public Account? account { get; set; }
    }
}
