using Web.Lib.Models;

namespace Web.Lib.Services.Spec
{
    public interface IAccountService
    {
        public Task<bool> NoAccountsExistAsync();
        public Task<Account> SignUpAsync(string username, string password, string displayName);
    }
}
