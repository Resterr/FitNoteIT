using FitNoteIT.Modules.Exercises.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitNoteIT.Modules.Exercises.Core.Persistence.Contexts;
internal sealed class ExercisesDbContext : DbContext
{
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<Record> Records { get; set; }

    public ExercisesDbContext(DbContextOptions<ExercisesDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Exercises");

        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}