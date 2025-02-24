using MediatR;

using MinimalApiUsingMediatR.Commaand;
using MinimalApiUsingMediatR.Model;

namespace MinimalApiUsingMediatR.Handlers;

public class CreateContactHandler(AppDbContext context) : IRequestHandler<CreateContactCommand, ContactItem>
{
    private readonly AppDbContext _context = context;

    public async Task<ContactItem> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        var contact = new ContactItem { Name = request.Name, Email = request.Email };
        _context.Contacts.Add(contact);
        await _context.SaveChangesAsync(cancellationToken);
        return contact;
    }
}
