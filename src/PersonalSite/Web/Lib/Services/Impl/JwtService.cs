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
        public JwtService(AppSettings settings, IHttpContextAccessor hca, IWebHostEnvironment env)
        {
            Settings = settings;
            HttpContextAccessor = hca;
            Environment = env;
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
            var ctx = HttpContextAccessor.HttpContext;
            if(ctx == null)
            {
                throw new InvalidOperationException("HttpContext was null; this function can only be called in the context of an HTTP request");
            }
            ctx.Response.Cookies.Append(AppConstants.ACCESS_TOKEN_COOKIE_KEY, token, new()
            {
                SameSite = SameSiteMode.Strict,
                Secure = Environment.IsProduction(),
                Expires = DateTimeOffset.UtcNow.AddMinutes(Settings.JwtTokenExpiryMinutes ?? AppConstants.DEFAULT_JWT_EXPIRY_MINUTES)
            });
        }
    }
}
