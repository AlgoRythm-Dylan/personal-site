using Microsoft.EntityFrameworkCore;
using Web.Lib.Services.Spec;

namespace Web.Lib.Services.Impl
{
    public class AccountService : IAccountService
    {
        private readonly AdminDbCtx Ctx;
        public AccountService(AdminDbCtx ctx)
        {
            Ctx = ctx;
        }

        public async Task<bool> NoAccountsExistAsync()
        {
            return !await Ctx.Accounts.AnyAsync();
        }
    }
}
