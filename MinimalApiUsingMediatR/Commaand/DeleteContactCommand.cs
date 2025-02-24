using MediatR;

namespace MinimalApiUsingMediatR.Commaand;

public record DeleteContactCommand(int Id) : IRequest<bool>;
