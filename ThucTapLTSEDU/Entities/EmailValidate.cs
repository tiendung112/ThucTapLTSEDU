using System.ComponentModel.DataAnnotations.Schema;

namespace ThucTapLTSEDU.Entities
{
    [Table("XacNhanEmail")]
    public class EmailValidate:BaseEntity
    {
        public int? AccountID {  get; set; }
        public Account? account { get; set; }
        public DateTime? ThoiGianHetHan { get; set; }
        public string? MaXacNhan { get; set; }
        public bool DaXacNhan { get; set; } = false;

    }
}
