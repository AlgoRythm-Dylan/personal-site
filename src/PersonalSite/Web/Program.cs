using Web.Lib;
using Web.Lib.Services;

namespace Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.SetupJwtAuth();

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddWebServices();

            var app = builder.Build();

            app.UseAuthentication();
            app.UseAuthorization();
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseRouting();
            

            app.MapStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();

            app.Run();
        }
    }
}
