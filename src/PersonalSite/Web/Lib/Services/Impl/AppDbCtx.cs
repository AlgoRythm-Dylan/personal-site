using Microsoft.EntityFrameworkCore;

namespace Web.Lib.Services.Impl
{
    public abstract class AppDbCtx : DbContext
    {
        private readonly string ConnectionString;
        public AppDbCtx(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }
            ConnectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseMySql(
                ConnectionString,
                ServerVersion.AutoDetect(ConnectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("PersonalSite");
        }
    }
}
