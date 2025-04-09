using Web.Lib.Models;

namespace Web.Lib.Services.Spec
{
    public interface IRefreshTokenService
    {
        public Task<RefreshToken> GenerateForAsync(int accountID);
        public Task SaveAsync(RefreshToken token);
        public Task InvalidateAsync(string token);
    }
}
