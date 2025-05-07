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
            builder.Services.AddRazorPages()
                            .AddViewOptions(options =>
                            {
                                options.HtmlHelperOptions.ClientValidationEnabled = false;
                            });
            builder.Services.AddWebServices();

            var app = builder.Build();

            app.UseRouting();

            app.UseTokenRefresher();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapRazorPages()
                .WithStaticAssets();

            app.Run();
        }
    }
}
