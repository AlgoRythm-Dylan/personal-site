using Microsoft.EntityFrameworkCore;
using PersonalSiteLib.Entities;

namespace PersonalSiteLib
{
    public class PersonalSiteCtx : DbContext
    {
        private readonly IDbSettings Settings;
        public PersonalSiteCtx(IDbSettings settings)
        {
            Settings = settings;
        }

        public DbSet<Image> Images { get; set; }
        public DbSet<Photographer> Photographers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySql(
                Settings.GetConnectionString(),
                ServerVersion.AutoDetect(Settings.GetConnectionString()));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("PersonalSite");
        }
    }
}
