using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Lib.Models
{
    [Table("LoginAudit")]
    public class LoginAudit
    {
        [Key]
        public int ID { get; set; }
        public int WasSuccess { get; set; }
        [Required]
        [MaxLength(256)]
        public string Username { get; set; }
        public int? AccountID { get; set; }
        public string? IpAddress { get; set; }

        [ForeignKey("AccountID")]
        public virtual Account? Account { get; set; }
    }
}
