using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalSiteLib.Entities
{
    [Table("Photographers")]
    public class Photographer
    {
        [Key]
        public int ID { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public DateTime DateAdded { get; set; } = DateTime.Now;
    }
}
