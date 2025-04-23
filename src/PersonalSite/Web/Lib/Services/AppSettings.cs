namespace Web.Lib.Services
{
    public class AppSettings
    {
        private readonly IConfiguration Config;
        public AppSettings(IConfiguration config)
        {
            Config = config;
        }
        private string GetNonEmptyPropertyOrFail(string key)
        {
            string? value = Config[key];
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidOperationException($"Required configuration key was missing or empty: {key}");
            }
            else
            {
                return value;
            }
        }
        private T? GetNullableProperty<T>(string key) where T : struct
        {
            var value = Config[key];
            if (value == null)
            {
                return null;
            }
            else
            {
                try
                {
                    return (T)Convert.ChangeType(value, typeof(T));
                }
                catch (Exception ex) when (ex is InvalidCastException || ex is InvalidCastException)
                {
                    throw new InvalidOperationException($"Invalid format for configuration key: {key}. Expected {typeof(T)}", ex);
                }
            }
        }
        public string ViewerConnectionString
        {
            get => GetNonEmptyPropertyOrFail("Application:ViewerConnectionString");
        }
        public string AdminConnectionString
        {
            get => GetNonEmptyPropertyOrFail("Application:AdminConnectionString");
        }
        public string DataLocation
        {
            get => GetNonEmptyPropertyOrFail("Application:DataLocation");
        }
        public string? DefaultPageTitle
        {
            get => Config["Application:DefaultPageTitle"];
        }

        public string JwtSecret
        {
            get => GetNonEmptyPropertyOrFail(AppConstants.JWT_SECRET_CONFIG_KEY);
        }
        public int? JwtTokenExpiryMinutes
        {
            get => GetNullableProperty<int>("Application:Security:JwtTokenExpiryMinutes");
        }

        public int? RefreshTokenExpiryMinutes
        {
            get => GetNullableProperty<int>("Application:Security:RefreshTokenExpiryMinutes");
        }
        public string Salt
        {
            get => GetNonEmptyPropertyOrFail("Application:Security:Salt");
        }
    }
}
