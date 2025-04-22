using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Lib.Models
{
    [Table("Collections")]
    public class Collection
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
