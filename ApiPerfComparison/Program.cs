
using ApiPerfComparison.Auth;
using ApiPerfComparison.Middleware;

using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddAuthorization();
builder.Services.AddApiKeySupport(new ApiKeyConstants());

var app = builder.Build();

app.MapControllers();
app.RegisterEndpoints();

// Register the middleware
//app.UseMiddleware<ValidationMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.Run();

public partial class Program { }