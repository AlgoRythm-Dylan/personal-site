using Microsoft.EntityFrameworkCore;
using Web.Lib.Models;
using Web.Lib.Services.Spec;

namespace Web.Lib.Services.Impl
{
    public class AccountService : IAccountService
    {
        private readonly AdminDbCtx Ctx;
        private readonly ISessionService SessionService;
        public AccountService(AdminDbCtx ctx, ISessionService loginService)
        {
            Ctx = ctx;
            SessionService = loginService;
        }

        public async Task<bool> NoAccountsExistAsync()
        {
            return !await Ctx.Accounts.AnyAsync();
        }

        public async Task<Account> SignUpAsync(string username, string password, string displayName)
        {
            var newRecord = new Account()
            {
                PasswordHash = SessionService.HashPassword(password),
                DisplayName = displayName,
                Username = username
            };
            await Ctx.Accounts.AddAsync(newRecord);
            await Ctx.SaveChangesAsync();
            await SessionService.CreateSessionAsAsync(newRecord);
            return newRecord;
        }
    }
}
