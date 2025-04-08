using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Lib.Models
{
    [Table("Accounts")]
    public class Account
    {
        [Key]
        public int ID { get; set; }
        public string? DisplayName { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}
