
using ApiPerfComparison.Auth;
using ApiPerfComparison.Middleware;

using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddApiKeySupport(builder.Configuration);


var app = builder.Build();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();
app.RegisterEndpoints();

// Register the middleware
//app.UseMiddleware<ValidationMiddleware>();


app.Run();

public partial class Program { }