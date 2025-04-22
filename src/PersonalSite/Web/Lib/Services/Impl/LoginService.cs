using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using Web.Lib.Models;
using Web.Lib.Services.Spec;

namespace Web.Lib.Services.Impl
{
    public class LoginService : ILoginService
    {
        private readonly AppSettings Settings;
        private readonly ViewerDbCtx Ctx;
        public LoginService(AppSettings settings, ViewerDbCtx ctx)
        {
            Settings = settings;
            Ctx = ctx;
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
                return await Ctx.Accounts.FirstAsync(account => account.Username.ToLower() == lowerUsername &&
                                                                account.PasswordHash == passwordHashed);
            }
            catch
            {
                return null;
            }
        }
    }
}
