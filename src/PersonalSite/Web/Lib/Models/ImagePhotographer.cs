using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Lib.Models
{
    [Table("ImagePhotographers")]
    [PrimaryKey(nameof(ImageID), nameof(PhotographerID))]
    public class ImagePhotographer
    {
        public int ImageID { get; set; }
        public int PhotographerID { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;

        [ForeignKey(nameof(PhotographerID))]
        public virtual Photographer Photographer { get; set; }
        [ForeignKey(nameof(ImageID))]
        public virtual Image Image { get; set; }
    }
}
