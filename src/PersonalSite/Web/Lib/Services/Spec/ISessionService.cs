using Web.Lib.Models;

namespace Web.Lib.Services.Spec
{
    public interface ISessionService
    {
        public string HashPassword(string password);
        public Task<Account?> LoginAsync(string username, string password);
        public Task CreateSessionAsAsync(Account account);
        public Task CycleTokensIfNecessary();
    }
}
