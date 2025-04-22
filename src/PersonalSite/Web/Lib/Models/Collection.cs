using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Lib.Models
{
    /// <summary>
    /// Collections are the way admins can curate photos into
    /// an organized "album"
    /// </summary>
    [Table("Collections")]
    public class Collection
    {
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// The public display name of the collection
        /// </summary>
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
