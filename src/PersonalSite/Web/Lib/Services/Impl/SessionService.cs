using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using Web.Lib.Models;
using Web.Lib.Services.Spec;

namespace Web.Lib.Services.Impl
{
    public class SessionService : ISessionService
    {
        private readonly AppSettings Settings;
        private readonly ViewerDbCtx Ctx;
        private readonly IJwtService JwtService;
        private readonly IRefreshTokenService RefreshTokenService;
        private readonly IHttpContextAccessor HttpContextAccessor;
        public SessionService(AppSettings settings, ViewerDbCtx ctx, IJwtService jwtService, IRefreshTokenService rts, IHttpContextAccessor hca)
        {
            Settings = settings;
            Ctx = ctx;
            JwtService = jwtService;
            RefreshTokenService = rts;
            HttpContextAccessor = hca;
        }

        public string HashPassword(string password)
        {
            return HashString(HashString(password) + Settings.Salt);
        }

        private static string HashString(string input)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToHexString(bytes);
        }

        public async Task<Account?> LoginAsync(string username, string password)
        {
            var lowerUsername = username.ToLower();
            var passwordHashed = HashPassword(password);
            try
            {
                var account = await Ctx.Accounts.FirstAsync(account => account.Username.ToLower() == lowerUsername &&
                                                                       account.PasswordHash == passwordHashed);
                await CreateSessionAsAsync(account);
                return account;
            }
            catch
            {
                return null;
            }
        }

        public async Task CreateSessionAsAsync(Account account)
        {
            var refreshToken = RefreshTokenService.GenerateFor(account.ID);
            await RefreshTokenService.SaveAsync(refreshToken);
            RefreshTokenService.WriteToClient(refreshToken.Token);
            var accessToken = JwtService.Generate(account.DisplayName ?? account.Username, account.ID);
            JwtService.WriteToClient(accessToken);
        }

        public async Task CycleTokensIfNecessary()
        {
            var accessToken = JwtService.ReadFromRequest();
            if(accessToken is null)
            {
                var refreshToken = await RefreshTokenService.FetchFromRequestAsync();
                if(refreshToken is not null)
                {
                    await RefreshTokenService.CycleForRequestAsync();
                }
            }
            else
            {
                // This function doesn't *validate* access tokens,
                // that's for the identity platform to do. This
                // service just refreshes them so they're available
                // for the identity platform when it needs them
                if (JwtService.IsExpired(accessToken))
                {
                    // Use the refresh token (VALIDATED) to get a new
                    // access token
                    var refreshToken = await RefreshTokenService.FetchFromRequestAsync();
                    if (refreshToken is not null)
                    {
                        await RefreshTokenService.CycleForRequestAsync();
                        var newAccessToken = JwtService.Generate(refreshToken.Account.DisplayName ?? refreshToken.Account.Username,
                                                                 refreshToken.AccountID);
                        JwtService.WriteToClient(newAccessToken);
                    }
                }
            }
        }

        public async Task LogoutAsync()
        {
            var context = HttpContextAccessor.HttpContextOrThrow();
            var refreshToken = context.Request.Cookies[AppConstants.REFRESH_TOKEN_COOKIE_KEY];
            if(refreshToken is not null)
            {
                await RefreshTokenService.InvalidateAsync(refreshToken);
            }
            context.Response.Cookies.Delete(AppConstants.REFRESH_TOKEN_COOKIE_KEY);
            context.Response.Cookies.Delete(AppConstants.ACCESS_TOKEN_COOKIE_KEY);
        }
    }
}
