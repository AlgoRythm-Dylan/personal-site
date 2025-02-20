using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalSiteLib.Entities
{
    [Table("ImagePhotographers")]
    [PrimaryKey(nameof(ImageID), nameof(PhotographerID))]
    public class ImagePhotographer
    {
        public int ImageID { get; set; }
        public int PhotographerID { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;

        [ForeignKey(nameof(PhotographerID))]
        public virtual Photographer Photographer { get; set; }
        [ForeignKey(nameof(ImageID))]
        public virtual Image Image { get; set; }
    }
}
