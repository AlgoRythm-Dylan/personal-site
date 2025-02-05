using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalSiteLib.Entities
{
    [Table("Images")]
    public class Image
    {
        public int ID { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int ViewCount { get; set; } = 0;
        public int Width { get; set; }
        public int Height { get; set; }
        public int Brightness { get; set; }
        public DateTime? CaptureDate { get; set; } = null;
        public string FileName { get; set; } = Guid.NewGuid().ToString();

        public DateTime UploadDate { get; set; } = DateTime.Now;
        public DateTime? DeletedDate { get; set; } = null;
    }
}
