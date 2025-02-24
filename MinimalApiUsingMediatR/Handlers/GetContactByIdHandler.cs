using MediatR;

using MinimalApiUsingMediatR.Model;
using MinimalApiUsingMediatR.Query;

namespace MinimalApiUsingMediatR.Handlers
{
    public class GetContactByIdHandler(AppDbContext context) : IRequestHandler<GetContactByIdQuery, ContactItem>
    {
        private readonly AppDbContext _context = context;

        public async Task<ContactItem> Handle(GetContactByIdQuery request, CancellationToken cancellationToken)
            => await _context.Contacts.FindAsync([request.Id], cancellationToken);
    }
}