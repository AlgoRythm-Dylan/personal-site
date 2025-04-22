using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Lib.Models
{
    [Table("Accounts")]
    public class Account
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(64)]
        public string? DisplayName { get; set; }
        [Required]
        [MaxLength(64)]
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}
