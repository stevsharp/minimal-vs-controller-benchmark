
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();
app.RegisterDishesEndpoints();

app.Run();

public partial class Program { }