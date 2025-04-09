using Web.Lib.Models;
using Web.Lib.Services.Spec;

namespace Web.Lib.Services.Impl
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly 
        public Task<RefreshToken> GenerateForAsync(int accountID)
        {
            throw new NotImplementedException();
        }

        public Task InvalidateAsync(string token)
        {
            throw new NotImplementedException();
        }
    }
}
