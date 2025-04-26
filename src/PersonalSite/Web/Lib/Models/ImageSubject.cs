using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Lib.Models
{
    [Table("ImageSubject")]
    [PrimaryKey(nameof(ImageID), nameof(SubjectID))]
    public class ImageSubject
    {
        public string ImageID { get; set; }
        public int SubjectID { get; set; }
        public QuantityBucket QuantityBucket { get; set; }
        public int IsPrimarySubject { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
