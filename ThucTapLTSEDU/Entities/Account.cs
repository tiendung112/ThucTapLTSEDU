using System.ComponentModel.DataAnnotations.Schema;

namespace ThucTapLTSEDU.Entities
{
    [Table(name:"account")]
    public class Account :BaseEntity
    {
        public string? user_name { get; set; }
        public string? avatar {  get; set; }
        public string? password { get;set; }
        public int? status { get; set; } = 1;

        //public int? decedecentralization_id { get; set; }
        public int? decedecentralizationID {  get; set; }
        public Decentralization? decedecentralization { get; set; }

        public string? ResetPasswordToken {  get; set; }
        public DateTime? ResetPasswordTokenExpiry { get; set; }

        public DateTime? created_at { get; set; } = DateTime.Now;
        public DateTime? updated_at { get; set;}
        public IEnumerable<User>? Users { get; set; }
        public IEnumerable<RefreshToken>? refreshTokens { get; set;}
        public IEnumerable<EmailValidate>? emailValidates {  get; set; } 
    }
}
