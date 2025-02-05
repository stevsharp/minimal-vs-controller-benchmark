using FluentValidation;

using Microsoft.AspNetCore.Mvc;

using System.ComponentModel.DataAnnotations;

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
