using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Lib.Models
{
    [Table("Images")]
    public class Image
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(256)]
        public string? Title { get; set; }
        [MaxLength(2048)]
        public string? Description { get; set; }
        [MaxLength(1024)]
        public string? AlterationsDescription { get; set; }
        public int? NoAlterations { get; set; }
        public int SourceWidth { get; set; }
        public int SourceHeight { get; set; }
        public int Brightness { get; set; }
        public DateTime? CaptureDate { get; set; } = null;
        public string FileName { get; set; } = Guid.NewGuid().ToString();
        public int? ISO { get; set; } = null;
        public int? ExposureTimeNumerator { get; set; } = null;
        public int? ExposureTimeDenominator { get; set; } = null;
        public float? Aperature { get; set; } = null;

        public DateTime UploadDate { get; set; } = DateTime.Now;
        public DateTime? DeletedDate { get; set; } = null;
    }
}
