using MediatR;
using MinimalApiUsingMediatR.Model;

namespace MinimalApiUsingMediatR.Query;

public record GetContactByIdQuery(int Id) : IRequest<ContactItem>;
