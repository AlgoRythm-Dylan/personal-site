using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Lib.Models
{
    [Table("CollectionImages")]
    [PrimaryKey(nameof(ImageID), nameof(CollectionID))]
    public class CollectionImage
    {
        public int ImageID { get; set; }
        public int CollectionID { get; set; }

        [ForeignKey(nameof(ImageID))]
        public virtual Image Image { get; set; }
        [ForeignKey(nameof(CollectionID))]
        public virtual Collection Collection { get; set; }
    }
}
