namespace Web.Lib.Services
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

        public string? JwtSecret
        {
            get
            {
                return Config[AppConstants.JWT_SECRET_CONFIG_KEY];
            }
        }

        public int? JwtTokenExpiryMinutes
        {
            get
            {
                var value = Config["Application:Security:JwtTokenExpiryMinutes"];
                if(value == null)
                {
                    return null;
                }
                else
                {
                    return int.Parse(value);
                }
            }
        }
    }
}
