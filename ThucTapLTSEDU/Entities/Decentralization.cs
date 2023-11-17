using System.ComponentModel.DataAnnotations.Schema;

namespace ThucTapLTSEDU.Entities
{
    [Table(name: "decentralization")]
    public class Decentralization : BaseEntity
    {
        public string? Authority_name { get; set; }
        public DateTime? created_at { get; set; } = DateTime.Now;
        public DateTime? updated_at { get; set; }
        public IEnumerable<Account>? accounts { get; set; }
    }
}
