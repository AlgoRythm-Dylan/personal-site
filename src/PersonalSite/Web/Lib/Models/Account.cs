using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Lib.Models
{
    /// <summary>
    /// An account is just a login. As of now, users are not allowed
    /// to register so these are all admin accounts.
    /// </summary>
    [Table("Accounts")]
    public class Account
    {
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// The name of the user/account.
        /// </summary>
        [MaxLength(64)]
        public string? DisplayName { get; set; }
        /// <summary>
        /// Used to log in. Ideally should never be displayed.
        /// </summary>
        [Required]
        [MaxLength(64)]
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}
