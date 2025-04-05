using Microsoft.EntityFrameworkCore;

namespace Web.Lib.Services.Impl
{
    public class AdminCtx : DbContext
    {
        private readonly AppSettings Settings;
        public AdminCtx(AppSettings settings)
        {
            Settings = settings;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if(Settings.AdminConnectionString == null)
            {
                throw new InvalidOperationException("Critical application setting missing: Application:AdminConnectionString");
            }

            optionsBuilder.UseMySql(
                Settings.AdminConnectionString,
                ServerVersion.AutoDetect(Settings.AdminConnectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("PersonalSite");
        }
    }
}
