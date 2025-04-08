namespace Web.Lib.Services.Spec
{
    public interface IJwtService
    {
        public string Generate(string displayName, int accountID);
        public void WriteToClient(string token);
    }
}
