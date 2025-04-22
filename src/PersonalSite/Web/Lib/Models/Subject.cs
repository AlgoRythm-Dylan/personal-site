using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Lib.Models
{
    [Table("Subjects")]
    public class Subject
    {
        [Key]
        public int ID { get; set; }
        public int? ParentID { get; set; } = null;
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;
        public DateTime? DeletedDate { get; set; } = null;
    }
}
