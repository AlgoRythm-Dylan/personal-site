using Microsoft.EntityFrameworkCore;
using Web.Lib.Models;

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

        public DbSet<Account> Accounts { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<PageView> PageViews { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ImageView> ImageViews { get; set; }
        public DbSet<ImageColor> ImageColors { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<ImageSubject> ImageSubjects { get; set; }
        public DbSet<Photographer> Photographers { get; set; }
        public DbSet<ImagePhotographer> ImagePhotographers { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<CollectionImage> CollectionImages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseMySql(
                ConnectionString,
                ServerVersion.AutoDetect(ConnectionString));
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("PersonalSite");
        }
    }
}
