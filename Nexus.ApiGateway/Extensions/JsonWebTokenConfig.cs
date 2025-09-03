using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Nexus.ApiGateway.Extensions;

public static class JsonWebTokenConfig
{
    public static void Configure(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(cfg =>
        {
            cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // check header and identity
            cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // return 401
            cfg.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; // general/default scheme
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = false;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8
                    .GetBytes(builder.Configuration["JWT:Key"]
                        ?? throw new ArgumentNullException("JWT empty key"))
                    ),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };
        });
    }
}
