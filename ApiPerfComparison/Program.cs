
using ApiPerfComparison.Middleware;

using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
var app = builder.Build();

app.MapControllers();
app.RegisterDishesEndpoints();

// Register the middleware
app.UseMiddleware<ValidationMiddleware>();

app.Run();

public partial class Program { }