using FitNoteIT.Modules.Exercises.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitNoteIT.Modules.Exercises.Core.Abstractions;

internal interface IExercisesDbContext
{
	DbSet<Category> Categories { get; }
	DbSet<Exercise> Exercises { get; }
	DbSet<Record> Records { get; }
	int SaveChanges();
	Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}