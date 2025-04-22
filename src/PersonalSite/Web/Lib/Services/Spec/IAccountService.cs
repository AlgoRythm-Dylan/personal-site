namespace Web.Lib.Services.Spec
{
    public interface IAccountService
    {
        public Task<bool> NoAccountsExistAsync();
    }
}
