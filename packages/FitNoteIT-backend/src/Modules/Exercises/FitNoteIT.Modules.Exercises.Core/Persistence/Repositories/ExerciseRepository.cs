using FitNoteIT.Modules.Exercises.Core.Abstractions.Repositories;
using FitNoteIT.Modules.Exercises.Core.Entities;
using FitNoteIT.Modules.Exercises.Core.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FitNoteIT.Modules.Exercises.Core.Persistence.Repositories;
internal sealed class ExerciseRepository : IExerciseRepository
{
    private readonly ExercisesDbContext _dbContext;

    public ExerciseRepository(ExercisesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Exercise> GetByIdAsync(Guid id)
    {
        var query = _dbContext.Exercises
            .AsQueryable()
            .SingleOrDefaultAsync(x => x.Id == id);

        return query;
    }

    public Task<Exercise> GetByNameAsync(string name)
    {
        var query = _dbContext.Exercises
            .AsQueryable()
            .SingleOrDefaultAsync(x => x.Name == name);

        return query;
    }

    public async Task<(List<Exercise> items, int totalItemCount)> PaginatedGetAllAsync(int pageSize, int pageNumber)
    {
        var baseQuery = _dbContext.Exercises
            .AsQueryable();

        var totalItemsCount = baseQuery.Count();

        var resultQuery = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();

        return (resultQuery, totalItemsCount);
    }

    public Task<List<Exercise>> GetAllAsync()
    {
        var resultQuery = _dbContext.Exercises
            .ToListAsync();

        return resultQuery;
    }

    public async Task AddAsync(Exercise exercise)
    {
        await _dbContext.Exercises.AddAsync(exercise);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Exercise exercise)
    {
        _dbContext.Exercises.Update(exercise);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Exercise exercise)
    {
        _dbContext.Exercises.Remove(exercise);
        await _dbContext.SaveChangesAsync();
    }
}
