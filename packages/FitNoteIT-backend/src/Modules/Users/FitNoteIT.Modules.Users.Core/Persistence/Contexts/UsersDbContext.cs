using FitNoteIT.Modules.Users.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitNoteIT.Modules.Users.Core.Persistence.Contexts;
internal sealed class UsersDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Users");

        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
