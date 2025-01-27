public static class EndpointRouteBuilderExtensions
{
    public static void RegisterDishesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/minimalapi/hello", async () => {

            return "Hello from Minimal API!";
        });

        app.MapPost("/minimalapi/data", async (MyData data) =>
        {
            return Results.Ok(data);
        });
    }
}
