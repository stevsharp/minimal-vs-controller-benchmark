using MediatR;
using Microsoft.EntityFrameworkCore;

using MinimalApiUsingMediatR.Commaand;
using MinimalApiUsingMediatR.Model;
using MinimalApiUsingMediatR.Query;

namespace MinimalApiUsingMediatR.Handlers;

public class UpdateContactHandler(AppDbContext context) : IRequestHandler<UpdateContactCommand, bool>
{
    private readonly AppDbContext _context = context;

    public async Task<bool> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
    {
        var contact = await _context.Contacts.FindAsync([request.Id], cancellationToken);
        if (contact == null) return false;

        contact.Name = request.Name;
        contact.Email = request.Email;

        return (await _context.SaveChangesAsync(cancellationToken)) > 0;
        
    }
}
