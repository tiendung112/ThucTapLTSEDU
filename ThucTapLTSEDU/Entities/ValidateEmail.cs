using System.ComponentModel.DataAnnotations.Schema;

namespace ThucTapLTSEDU.Entities
{
    [Table(name:"validat_mail")]
    public class ValidateEmail:BaseEntity
    {
        public int accountID { get; set; }
        public Account? account { get; set; }
        public DateTime? ThoiGianHetHan { get; set; }
        public string? MaXacNhan { get; set; }
        public bool DaXacNhan { get; set; } = false;
    }
}
