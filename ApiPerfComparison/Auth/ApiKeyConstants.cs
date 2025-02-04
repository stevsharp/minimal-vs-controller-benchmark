using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ApiPerfComparison.Auth;

public class ApiKeyConstants
{
    private string jwtKey = "your-secure-key-here-1234567890_xxsfa4564@##%^&&";
    private string issuer = "yourdomain.com";
    private string audience = "yourdomain.com";

    public string JwtKey => jwtKey;
    public  string Issuer => issuer;
    public  string Audience => audience;
}

public static class AuthExtensions
{
    public static void AddApiKeySupport(this IServiceCollection services, ApiKeyConstants apiKeyConstants)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
         .AddJwtBearer(options =>
         {
             options.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidateIssuer = false,
                 ValidateAudience = false,
                 ValidateLifetime = false,
                 ValidateIssuerSigningKey = false,
                 ValidIssuer = apiKeyConstants.Issuer,
                 ValidAudience = apiKeyConstants.Audience,
                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(apiKeyConstants.JwtKey))
             };
         });
    }

}