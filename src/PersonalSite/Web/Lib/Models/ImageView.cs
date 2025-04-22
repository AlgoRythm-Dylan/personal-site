using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Lib.Models
{
    [Table("ImageViews")]
    public class ImageView
    {
        public int ImageID { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;

        [ForeignKey(nameof(ImageID))]
        public virtual Image Image { get; set; }
    }
}
