using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Lib.Models
{
    [Table("RefreshTokens")]
    public class RefreshToken
    {
        [Key]
        public string Token { get; set; }
        public int AccountID { get; set; }
        public DateTime Expiry { get; set; }
    }
}
