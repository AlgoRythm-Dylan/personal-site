namespace Web.Lib.Services.Spec
{
    public interface IJwtService
    {
        public string Generate(string displayName, int accountID);
        public void WriteToClient(string token);
        public bool IsExpired(string token);
        public string? ReadFromRequest();
        public Task CycleForRequestAsync();
    }
}
