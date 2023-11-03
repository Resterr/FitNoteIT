using System.Reflection;
using FitNoteIT.Modules.Exercises.Core.Abstractions;
using FitNoteIT.Modules.Exercises.Core.Entities;
using FitNoteIT.Shared.Database.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace FitNoteIT.Modules.Exercises.Core.Persistence;

public class ExercisesDbContext : DbContext, IExercisesDbContext
{
	private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

	public ExercisesDbContext(DbContextOptions<ExercisesDbContext> options, AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) : base(options)
	{
		_auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
	}

	public DbSet<Category> Categories => Set<Category>();
	public DbSet<Exercise> Exercises => Set<Exercise>();
	public DbSet<Record> Records => Set<Record>();

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

		builder.Entity<Exercise>()
			.HasOne(x => x.Category)
			.WithMany(x => x.Exercises)
			.HasForeignKey(x => x.CategoryId)
			.OnDelete(DeleteBehavior.SetNull);

		builder.HasDefaultSchema("exercises");

		base.OnModelCreating(builder);
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
	}
}