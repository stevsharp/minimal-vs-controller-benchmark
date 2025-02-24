using MediatR;

namespace MinimalApiUsingMediatR.Commaand;

public record UpdateContactCommand : IRequest<bool>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
