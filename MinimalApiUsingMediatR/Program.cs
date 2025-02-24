using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

using MinimalApiUsingMediatR;
using MinimalApiUsingMediatR.Endpoints;

using System.Reflection;

try
{

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>


    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Minimal API Using MediatR", Version = "v1" });
    });


    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseInMemoryDatabase("ContactsDb"));
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

    var app = builder.Build();

    app.MapContactEndpoints();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minimal API Using MediatR v1"));
        app.MapOpenApi();
    }

    app.UseHttpsRedirection();

    app.Run();
}
catch (Exception ex)
{
    var exception = ex;
    throw;
}



