using MediatR;

using MinimalApiUsingMediatR.Model;

namespace MinimalApiUsingMediatR.Commaand;

public record CreateContactCommand(string Name, string Email) : IRequest<ContactItem>;