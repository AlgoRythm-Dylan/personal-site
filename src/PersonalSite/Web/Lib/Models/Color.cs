using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Lib.Models
{
    /// <summary>
    /// All colors from all photos. Two or more photos might share the same color.
    /// </summary>
    [Table("Colors")]
    public class Color
    {
        [Key]
        public int ID { get; set; } // Just because a triple composite key is a LITTLE messy.
        public short Red { get; set; }
        public short Green { get; set; }
        public short Blue { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
