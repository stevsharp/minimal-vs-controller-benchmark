using JwtAuthApp.JWT;

using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TestJwt.Identity;

public class IdentityService(JwtConfiguration config)
{
    private readonly JwtConfiguration _config = config;

    public async Task<string> GenerateToken(string username)
    {
        await Task.Delay(100); // simulate db call

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, "123456"),
            new Claim(JwtRegisteredClaimNames.Email, "admin@admin.gr"),
            new Claim(JwtRegisteredClaimNames.PreferredUsername, username)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config.Issuer,
            audience: _config.Audience,
            claims: claims,
            expires: DateTime.Now.AddDays(_config.ExpireDays),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}