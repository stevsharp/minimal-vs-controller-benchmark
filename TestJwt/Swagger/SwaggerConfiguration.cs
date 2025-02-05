using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace TestJwt.Swagger;

public static class SwaggerConfiguration
{
    public static OpenApiSecurityScheme Scheme => new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer",
        Reference = new OpenApiReference
        {
            Id = "Bearer",
            Type = ReferenceType.SecurityScheme,
        },
    };

    public static void Configure(SwaggerGenOptions option)
    {
        option.ResolveConflictingActions(apiDesc => apiDesc.First());
        option.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
        option.AddSecurityDefinition(Scheme.Reference.Id, Scheme);
        option.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            { Scheme, Array.Empty<string>() },
        });
    }
}