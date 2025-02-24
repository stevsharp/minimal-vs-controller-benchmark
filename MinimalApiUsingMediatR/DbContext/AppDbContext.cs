using Microsoft.EntityFrameworkCore;

using MinimalApiUsingMediatR.Model;

namespace MinimalApiUsingMediatR;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<ContactItem> Contacts { get; set; }
}
