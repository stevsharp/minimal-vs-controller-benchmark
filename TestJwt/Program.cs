using JwtAuthApp.JWT;

using TestJwt.Identity;
using TestJwt.Swagger;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<JwtConfiguration>();
builder.Services.AddScoped<IdentityService>();


builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddSwaggerGen(SwaggerConfiguration.Configure);

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "NY", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Lviv"
};


app.MapPost("/login", async (LoginRequest request, IdentityService identityService, IConfiguration config, ILogger<Program> logger) =>
{
    // Retrieve admin credentials from configuration (Optional for flexibility)
    var adminUsername = config["Auth:AdminUsername"] ?? "admin";
    var adminPassword = config["Auth:AdminPassword"] ?? "password";

    // Validate user credentials
    if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
    {
        logger.LogWarning("Login failed: Empty username or password.");
        return Results.BadRequest(new { message = "Username and password are required." });
    }

    var userIsAuthenticated = request.Username == adminUsername && request.Password == adminPassword;

    if (!userIsAuthenticated)
    {
        logger.LogWarning("Login failed for user: {Username}", request.Username);
        return Results.Unauthorized();
    }

    // Generate JWT token
    var token = await identityService.GenerateToken(request.Username);

    logger.LogInformation("User {Username} authenticated successfully.", request.Username);

    return Results.Ok(new
    {
        message = "Login successful",
        token
    });
})
.AllowAnonymous();

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.RequireAuthorization()
.WithName("GetWeatherForecast")
.WithOpenApi();


app.Run();


