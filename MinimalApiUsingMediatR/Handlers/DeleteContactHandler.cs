using MediatR;

using MinimalApiUsingMediatR.Commaand;

namespace MinimalApiUsingMediatR.Handlers
{
    public class DeleteContactHandler(AppDbContext context) : IRequestHandler<DeleteContactCommand, bool>
    {
        private readonly AppDbContext _context = context;

        public async Task<bool> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
        {
            var contact = await _context.Contacts.FindAsync([request.Id], cancellationToken);
            if (contact == null) return false;
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}