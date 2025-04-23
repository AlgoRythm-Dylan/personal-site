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
        public string ID { get; set; } = Crypto.RandomToken();
        /// <summary>
        /// The public display name of the collection
        /// </summary>
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }
        [MaxLength(1024)]
        public string? Description { get; set; }
        /// <summary>
        /// This collection is only viewable to the user who created it
        /// </summary>
        public int IsUnlisted { get; set; } = 0;

        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
