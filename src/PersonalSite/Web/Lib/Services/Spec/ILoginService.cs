using Web.Lib.Models;

namespace Web.Lib.Services.Spec
{
    public interface ILoginService
    {
        public string HashPassword(string password);
        public Task<Account?> LoginAsync(string username, string password);
    }
}
