using Web.Lib.Services.Spec;

namespace Web.Lib
{
    public class TokenRefrehsingMiddleware
    {
        private readonly RequestDelegate Next;
        public TokenRefrehsingMiddleware(RequestDelegate next)
        {
            Next = next;
        }
        public async Task InvokeAsync(HttpContext context, IJwtService jwtService, IRefreshTokenService refreshService)
        {
            var accessToken = jwtService.ReadFromRequest();
            if(accessToken is not null)
            {
                // If the token is present, only cycle if it's expired
                if (jwtService.IsExpired(accessToken))
                {
                    await jwtService.CycleForRequestAsync();
                }
            }
            else
            {
                // If the ACCESS token is not present, the refresh token might be
                // this procedure tries to create a new access token if the refresh
                // token is present and valid. It will also cycle the refresh token.
                await jwtService.CycleForRequestAsync();
            }
            await Next(context);
        }
    }
    public static class TokenRefreshingMiddlewareAdder
    {
        public static void UseTokenRefresher(this WebApplication app)
        {
            app.UseMiddleware<TokenRefrehsingMiddleware>();
        }
    }
}
