using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Lib.Models
{
    [Table("Files")]
    public class File
    {
        [Key]
        public string ID { get; set; } = Crypto.RandomToken();
        [MaxLength(64)]
        public string? Name { get; set; }
        [MaxLength(256)]
        public string? Description { get; set; }
        [MaxLength(64)]
        public string SystemFileName { get; set; }
        [MaxLength(64)]
        public string? OriginalFileName { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
