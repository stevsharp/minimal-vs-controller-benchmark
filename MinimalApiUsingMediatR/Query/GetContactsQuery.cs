using MediatR;

using MinimalApiUsingMediatR.Model;

namespace MinimalApiUsingMediatR.Query;

public record GetContactsQuery : IRequest<List<ContactItem>>;