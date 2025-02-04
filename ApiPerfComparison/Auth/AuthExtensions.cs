using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ApiPerfComparison.Auth;

public static class AuthExtensions
{
    public static void AddApiKeySupport(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false; // Only for local testing
            options.SaveToken = true;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                ClockSkew = TimeSpan.Zero // Prevents expired tokens from being valid
            };
        });

        //services.AddAuthorizationBuilder()
        //    .SetFallbackPolicy(new AuthorizationPolicyBuilder()
        //        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        //        .RequireAuthenticatedUser()
        //        .Build());

        services.AddAuthorization();
    }

}