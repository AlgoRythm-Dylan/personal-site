using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Lib.Models
{
    [Table("PageViews")]
    public class PageView
    {
        [Key]
        public int PageViewID { get; set; }
        [MaxLength(64)]
        public string PageName { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
