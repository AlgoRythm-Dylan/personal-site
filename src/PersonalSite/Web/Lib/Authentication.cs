using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Web.Lib
{
    public static class Authentication
    {
        public static void SetupJwtAuth(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var secret = builder.Configuration[AppConstants.JWT_SECRET_CONFIG_KEY];
                    if (secret == null)
                    {
                        throw new InvalidOperationException("Critical configuration property missing: Application:Security:JwtSecret");
                    }
                    var bytes = Encoding.UTF8.GetBytes(secret);
                    if (bytes.Length < 32)
                    {
                        throw new InvalidOperationException($"Configuration property JwtSecret (Application:Security:JwtSecret) needs to be at least 32 bytes long, found {bytes.Length} instead");
                    }
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
                    options.TokenValidationParameters = new()
                    {
                        ValidateLifetime = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero,
                        IssuerSigningKey = key,
                        NameClaimType = JwtRegisteredClaimNames.Name,
                        RoleClaimType = "roles"
                    };
                    options.Events = new()
                    {
                        OnMessageReceived = async ctx =>
                        {
                            ctx.Token = ctx.Request.Cookies[AppConstants.ACCESS_TOKEN_COOKIE_KEY];
                        }
                    };
                });
        }
    }
}
