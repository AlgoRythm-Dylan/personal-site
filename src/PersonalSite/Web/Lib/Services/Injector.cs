using Web.Lib.Services.Impl;
using Web.Lib.Services.Spec;

namespace Web.Lib.Services
{
    public static class Injector
    {
        public static void AddWebServices(this IServiceCollection services)
        {
            services.AddTransient<AppSettings>();
            services.AddTransient<AdminDbCtx>();
            services.AddTransient<ViewerDbCtx>();

            services.AddHttpContextAccessor();

            services.AddTransient<IJwtService, JwtService>();
            services.AddTransient<IRefreshTokenService, RefreshTokenService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ISessionService, SessionService>();
            services.AddTransient<IObjectViewService, ObjectViewService>();
        }
    }
}
