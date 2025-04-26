using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Lib.Models
{
    [Table("ImageColors")]
    [PrimaryKey(nameof(ImageID), nameof(ColorID))]
    public class ImageColor
    {
        public string ImageID { get; set; }
        public int ColorID { get; set; }

        [ForeignKey(nameof(ImageID))]
        public virtual Image Image { get; set; }
        [ForeignKey(nameof(ColorID))]
        public virtual Color Color { get; set; }
    }
}
