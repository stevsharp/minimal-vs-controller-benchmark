using FluentValidation;

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
        }).AllowAnonymous();

        app.MapPost("/minimalapi/data", async (MyData data) =>
        {
            return Results.Ok(data);
        }).AllowAnonymous(); 
        
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
        }).AllowAnonymous(); 

        app.MapPost("/login", (UserLogin user, IConfiguration configuration) =>
        {
            if (user.Username == "admin" && user.Password == "password") 
            {
                var claims = new[]
                 {
                    new Claim(ClaimTypes.NameIdentifier, user.Username)
                };

                    var token = new JwtSecurityToken
                    (
                        issuer: configuration["Jwt:Issuer"],
                        audience: configuration["Jwt:Audience"],
                        claims: claims,
                        expires: DateTime.UtcNow.AddDays(60),
                        notBefore: DateTime.UtcNow,
                        signingCredentials: new SigningCredentials(
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                            SecurityAlgorithms.HmacSha256)
                    );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return Results.Ok(new { Token = tokenString });
            }

            return Results.Unauthorized();
        }).AllowAnonymous();

        app.MapGet("/secure", (HttpContext httpContext) =>
        {
            var user = httpContext.User;
            Console.WriteLine($"User authenticated: {user.Identity?.IsAuthenticated}");
            Console.WriteLine($"User name: {user.Identity?.Name}");
            Console.WriteLine($"Roles: {string.Join(",", user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value))}");

            return "This is a secured endpoint!";
        }).RequireAuthorization();

        app.MapGet("/notSecure1", () => "This is a secured endpoint!")
            .AllowAnonymous();
    

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
        }).AllowAnonymous(); 

    }
}
