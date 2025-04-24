using Microsoft.EntityFrameworkCore;

namespace Web.Lib.Models
{
    [PrimaryKey(nameof(FileID), nameof(ImageID))]
    public class ImageAttachment
    {
        public string FileID { get; set; }
        public string ImageID { get; set; }
    }
}
