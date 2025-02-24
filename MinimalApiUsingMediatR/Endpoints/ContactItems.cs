using MediatR;
using MinimalApiUsingMediatR.Commaand;
using MinimalApiUsingMediatR.Query;

namespace MinimalApiUsingMediatR.Endpoints;

public static class ContactEndpoints
{
    public static void MapContactEndpoints(this WebApplication app)
    {
        app.MapGet("/contacts", async (IMediator mediator) => await mediator.Send(new GetContactsQuery()));
        app.MapGet("/contacts/{id}", async (IMediator mediator, int id) => await mediator.Send(new GetContactByIdQuery(id)));
        app.MapPost("/contacts", async (IMediator mediator, CreateContactCommand command) => await mediator.Send(command));
        app.MapPut("/contacts/{id}", async (IMediator mediator, int id, UpdateContactCommand command) =>
        {
            command.Id = id;
            return await mediator.Send(command);
        });
        app.MapDelete("/contacts/{id}", async (IMediator mediator, int id) => await mediator.Send(new DeleteContactCommand(id)));
    }
}