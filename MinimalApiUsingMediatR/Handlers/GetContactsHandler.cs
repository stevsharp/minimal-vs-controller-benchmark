using MediatR;

using Microsoft.EntityFrameworkCore;

using MinimalApiUsingMediatR.Model;
using MinimalApiUsingMediatR.Query;

namespace MinimalApiUsingMediatR.Handlers;

public class GetContactsHandler(AppDbContext context) : IRequestHandler<GetContactsQuery, List<ContactItem>>
{
    private readonly AppDbContext _context = context;

    public async Task<List<ContactItem>> Handle(GetContactsQuery request, CancellationToken cancellationToken)
        => await _context.Contacts.ToListAsync(cancellationToken);
}
