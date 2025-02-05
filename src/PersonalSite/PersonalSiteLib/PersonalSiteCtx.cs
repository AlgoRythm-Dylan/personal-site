using Microsoft.EntityFrameworkCore;
using PersonalSiteLib.Entities;

namespace PersonalSiteLib
{
    public class PersonalSiteCtx : DbContext
    {
        public DbSet<Image> Images { get; set; }
    }
}
