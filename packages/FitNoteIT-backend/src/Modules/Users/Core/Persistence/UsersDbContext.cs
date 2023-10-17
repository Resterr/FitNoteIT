using System.Reflection;
using FitNoteIT.Modules.Users.Core.Entities;
using FitNoteIT.Shared.Database.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace FitNoteIT.Modules.Users.Core.Persistence;
public class UsersDbContext : DbContext
{
	private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

	public UsersDbContext(DbContextOptions<UsersDbContext> options, AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) : base(options)
	{
		_auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
	}

	public DbSet<User> Users => Set<User>();
	public DbSet<Role> Roles => Set<Role>();

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

		builder.Entity<User>()
		   .HasMany(x => x.Roles)
		   .WithMany(x => x.Users)
		   .UsingEntity(x =>
				x.ToTable("UserRole")
		);

		builder.HasDefaultSchema("users");

		base.OnModelCreating(builder);
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
	}
}
