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
        public SessionService(AppSettings settings, ViewerDbCtx ctx, IJwtService jwtService, IRefreshTokenService rts)
        {
            Settings = settings;
            Ctx = ctx;
            JwtService = jwtService;
            RefreshTokenService = rts;
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
                
            }
        }
    }
}
