using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace ApiPerfComparison.Middleware;

public class ValidationMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Method == HttpMethods.Post || context.Request.Method == HttpMethods.Put)
        {
            context.Request.EnableBuffering();

            using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
            var body = await reader.ReadToEndAsync();

            context.Request.Body.Position = 0;

            if (!string.IsNullOrEmpty(body))
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var user = JsonSerializer.Deserialize<UserDto>(body, options);

                if (user != null)
                {
                    var validationResults = new List<ValidationResult>();
                    var validationContext = new ValidationContext(user);

                    if (!Validator.TryValidateObject(user, validationContext, validationResults, true))
                    {
                        var errors = validationResults.ToDictionary(
                            v => v.MemberNames.FirstOrDefault() ?? "Error",
                            v => new string[] { v.ErrorMessage! });

                        var errorResponse = new { Message = "Validation failed", Errors = errors };

                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsJsonAsync(errorResponse);
                        return; // Stop processing further
                    }
                }
            }

            await _next(context);
        }
    }
}
