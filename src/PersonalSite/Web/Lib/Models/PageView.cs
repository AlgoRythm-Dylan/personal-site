using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Lib.Models
{
    [Keyless]
    [Table("PageViews")]
    public class PageView
    {
        [MaxLength(64)]
        public string PageName { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
