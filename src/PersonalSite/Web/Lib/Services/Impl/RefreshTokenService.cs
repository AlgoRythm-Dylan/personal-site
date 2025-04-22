using Microsoft.EntityFrameworkCore;
using Web.Lib.Models;
using Web.Lib.Services.Spec;

namespace Web.Lib.Services.Impl
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly AdminDbCtx Ctx;
        private readonly AppSettings Settings;
        private readonly IHttpContextAccessor HttpContextAccessor;
        private readonly IWebHostEnvironment WebHostEnvironment;
        public RefreshTokenService(AdminDbCtx ctx, AppSettings settings, IHttpContextAccessor hca, IWebHostEnvironment whe)
        {
            Ctx = ctx;
            Settings = settings;
            HttpContextAccessor = hca;
            WebHostEnvironment = whe;
        }
        public RefreshToken GenerateFor(int accountID)
        {
            var newToken = new RefreshToken()
            {
                AccountID = accountID,
                Expiry = DateTime.UtcNow.AddMinutes(Settings.RefreshTokenExpiryMinutes ?? AppConstants.DEFAULT_REFRESH_TOKEN_EXPIRY_MINUTES)
            };
            return newToken;
        }

        /// <summary>
        /// Remove token from database, invalidating it.
        /// </summary>
        /// <param name="token">The token to invalidate.</param>
        public async Task InvalidateAsync(string token)
        {
            var tokenEntity = await Ctx.RefreshTokens.FirstAsync(x => x.Token == token);
            Ctx.RefreshTokens.Remove(tokenEntity);
            await Ctx.SaveChangesAsync();
        }

        /// <summary>
        /// Add refresh token to the database.
        /// </summary>
        /// <param name="token">New token to be added.</param>
        public async Task SaveAsync(RefreshToken token)
        {
            await Ctx.RefreshTokens.AddAsync(token);
            await Ctx.SaveChangesAsync();
        }

        /// <summary>
        /// If possible, makes a database call to get the correct RefreshToken
        /// associated with the current HttpContext.
        /// </summary>
        /// <returns>The RefreshToken associated with this HttpContext or null if none.</returns>
        public async Task<RefreshToken?> FetchFromRequestAsync()
        {
            var httpContext = HttpContextAccessor.HttpContextOrThrow();
            var token = httpContext.Request.Headers[AppConstants.REFRESH_TOKEN_COOKIE_KEY].FirstOrDefault();
            if(token is null)
            {
                return null;
            }
            else
            {
                return await Ctx.RefreshTokens.FirstOrDefaultAsync(tok => tok.Token == token && tok.Expiry < DateTime.UtcNow);
            }
        }

        /// <summary>
        /// Write the refresh token string to the user's cookies.
        /// </summary>
        /// <param name="token">RefreshToken.Token property.</param>
        public void WriteToClient(string token)
        {
            var httpContext = HttpContextAccessor.HttpContextOrThrow();
            httpContext.Response.Cookies.Append(AppConstants.REFRESH_TOKEN_COOKIE_KEY, token, new()
            {
                Expires = DateTime.UtcNow.AddMinutes(Settings.RefreshTokenExpiryMinutes ?? AppConstants.DEFAULT_REFRESH_TOKEN_EXPIRY_MINUTES),
                HttpOnly = true,
                Secure = WebHostEnvironment.IsProduction(),
                SameSite = SameSiteMode.Strict
            });
        }

        /// <summary>
        /// Fetches the current refresh token from the request and invalidates it,
        /// replacing it with a new one. If the current refresh token is not valid
        /// or does not exist, returns null.
        /// </summary>
        /// <returns>New refresh token if current is valid, null otherwise.</returns>
        public async Task<RefreshToken?> CycleForRequestAsync()
        {
            var current = await FetchFromRequestAsync();
            if(current is null)
            {
                return null;
            }
            else
            {
                await InvalidateAsync(current.Token);
                var newRefreshToken = GenerateFor(current.AccountID);
                await SaveAsync(newRefreshToken);
                WriteToClient(newRefreshToken.Token);
                return newRefreshToken;
            }
        }
    }
}
