using FitNoteIT.Modules.Users.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitNoteIT.Modules.Users.Core.Abstractions;

internal interface IUsersDbContext
{
	DbSet<User> Users { get; }
	DbSet<Role> Roles { get; }
	Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}