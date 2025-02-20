using PersonalSiteLib;

namespace Management.Services
{
    public class DbSettings : IDbSettings
    {
        public string GetConnectionString()
        {
            return Environment.GetEnvironmentVariable("PERSONALSITE_CS_MGMT") ?? "";
        }
    }
}
