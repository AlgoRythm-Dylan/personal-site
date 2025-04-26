using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Lib.Models
{
    [Table("ImageViews")]
    [Keyless]
    public class ImageView
    {
        public string ImageID { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;

        [ForeignKey(nameof(ImageID))]
        public virtual Image Image { get; set; }
    }
}
