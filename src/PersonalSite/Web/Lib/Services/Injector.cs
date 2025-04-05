using Web.Lib.Services.Impl;
using Web.Lib.Services.Spec;

namespace Web.Lib.Services
{
    public static class Injector
    {
        public static void AddWebServices(this IServiceCollection services)
        {
            services.AddTransient<AppSettings>();
        }
    }
}
