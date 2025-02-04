using ApiPerfComparison.Auth;

using FluentValidation;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public static class EndpointRouteBuilderExtensions
{
    public static void RegisterEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/minimalapi/hello", async () => {

            return "Hello from Minimal API!";
        });

        app.MapPost("/minimalapi/data", async (MyData data) =>
        {
            return Results.Ok(data);
        });
        
        app.MapPost("minimalapi/create", ([FromBody] UserDto user) =>
        {
            var validationResults = new List<ValidationResult>();

            var context = new ValidationContext(user);

            if (!Validator.TryValidateObject(user, context, validationResults, true))
            {
                var errors = validationResults.ToDictionary(
                            v => v.MemberNames.FirstOrDefault() ?? "Error",
                            v => new string[] { v.ErrorMessage! });

                return Results.BadRequest(new { Message = "Validation failed", Errors = errors });
            }

            return Results.Ok($"User {user.Name} created successfully!");
        });

        app.MapPost("/login", (UserLogin user) =>
        {
            var apiKeyConstants = new ApiKeyConstants();

            if (user.Username == "admin" && user.Password == "password") 
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(apiKeyConstants.JwtKey);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Role, "Admin") 
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    Issuer = apiKeyConstants.Issuer,
                    Audience = apiKeyConstants.Audience,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Results.Ok(new { Token = tokenString });
            }

            return Results.Unauthorized();
        });

        app.MapGet("/secure", [Authorize] () => "This is a secured endpoint!")
            .RequireAuthorization();

        app.MapGet("/secure1", () => "This is a secured endpoint!");
    

        app.MapPost("minimalapi/createV1", ([FromBody] UserDto user, IValidator<UserDto> validator) =>
        {
            var validationResult = validator.Validate(user);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray());
                return Results.BadRequest(new { Message = "Validation failed", Errors = errors });
            }

            return Results.Ok($"User {user.Name} created successfully!");
        });

    }
}
