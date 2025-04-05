namespace Web.Lib.Services.Impl
{
    public class AppSettings
    {
        private readonly IConfiguration Config;
        public AppSettings(IConfiguration config)
        {
            Config = config;
        }
        public string? GuestConnectionString
        {
            get
            {
                return Config["Application:GuestConnectionString"];
            }
        }
        public string? AdminConnectionString
        {
            get
            {
                return Config["Application:AdminConnectionString"];
            }
        }
        public string? DataLocation
        {
            get
            {
                return Config["Application:DataLocation"];
            }
        }
        public string? DefaultPageTitle
        {
            get
            {
                return Config["Application:DefaultPageTitle"];
            }
        }
    }
}
