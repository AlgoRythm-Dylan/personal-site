namespace Web.Lib
{
    public static class AppConstants
    {
        public static readonly string JWT_SECRET_CONFIG_KEY = "Application:Security:JwtSecret";
        public static readonly string ACCESS_TOKEN_COOKIE_KEY = "access_token";
        public static readonly string REFRESH_TOKEN_COOKIE_KEY = "refresh_token";
        public static readonly int DEFAULT_JWT_EXPIRY_MINUTES = 15;
        public static readonly int DEFAULT_REFRESH_TOKEN_EXPIRY_MINUTES = 20160; // Two weeks
    }
}
