using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Web.Lib.Services.Spec;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace Web.Lib.Services.Impl
{
    public class JwtService : IJwtService
    {
        private readonly AppSettings Settings;
        private readonly IHttpContextAccessor HttpContextAccessor;
        private readonly IWebHostEnvironment Environment;
        private readonly IRefreshTokenService RefreshTokenService;
        public JwtService(AppSettings settings, IHttpContextAccessor hca, IWebHostEnvironment env, IRefreshTokenService rts)
        {
            Settings = settings;
            HttpContextAccessor = hca;
            Environment = env;
            RefreshTokenService = rts;
        }

        public string Generate(string displayName, int accountID)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, displayName),
                new Claim(JwtRegisteredClaimNames.Sub, accountID.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Settings.JwtSecret!));
            var expiryMinutes = Settings.JwtTokenExpiryMinutes ?? AppConstants.DEFAULT_JWT_EXPIRY_MINUTES; // Optional config value
            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public void WriteToClient(string token)
        {
            var ctx = HttpContextAccessor.HttpContextOrThrow();
            ctx.Response.Cookies.Append(AppConstants.ACCESS_TOKEN_COOKIE_KEY, token, new()
            {
                SameSite = SameSiteMode.Strict,
                Secure = Environment.IsProduction(),
                Expires = DateTimeOffset.UtcNow.AddMinutes(Settings.JwtTokenExpiryMinutes ?? AppConstants.DEFAULT_JWT_EXPIRY_MINUTES)
            });
        }

        public bool IsExpired(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var parsedToken = tokenHandler.ReadToken(token);
            return parsedToken.ValidTo < DateTime.UtcNow;
        }

        public string? ReadFromRequest()
        {
            var context = HttpContextAccessor.HttpContextOrThrow();
            return context.Request.Cookies[AppConstants.ACCESS_TOKEN_COOKIE_KEY];
        }

        public async Task CycleForRequestAsync()
        {
            var context = HttpContextAccessor.HttpContextOrThrow();
            var newRefreshToken = await RefreshTokenService.CycleForRequestAsync();
            if(newRefreshToken is not null)
            {
                var newAccessToken = Generate(newRefreshToken.Account.DisplayName ?? newRefreshToken.Account.Username, newRefreshToken.AccountID);
                WriteToClient(newAccessToken);
                context.Items[AppConstants.NEW_TOKEN_KEY] = newAccessToken;
            }
        }
    }
}
