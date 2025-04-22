using Web.Lib.Models;

namespace Web.Lib.Services.Spec
{
    public interface IRefreshTokenService
    {
        public RefreshToken GenerateFor(int accountID);
        public Task SaveAsync(RefreshToken token);
        public Task InvalidateAsync(string token);
        public Task<RefreshToken?> FetchFromRequestAsync();
        public void WriteToClient(string token);
        public Task<RefreshToken?> CycleForRequestAsync();
    }
}
