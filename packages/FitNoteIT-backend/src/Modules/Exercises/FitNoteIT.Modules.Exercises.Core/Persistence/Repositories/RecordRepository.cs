using FitNoteIT.Modules.Exercises.Core.Abstractions.Repositories;
using FitNoteIT.Modules.Exercises.Core.Entities;
using FitNoteIT.Modules.Exercises.Core.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FitNoteIT.Modules.Exercises.Core.Persistence.Repositories;
internal sealed class RecordRepository : IRecordRepository
{
    private readonly ExercisesDbContext _dbContext;

    public RecordRepository(ExercisesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Record> GetByIdAsync(Guid recordId, Guid userId)
    {
        var query = _dbContext.Records
            .Include(x => x.Exercise)
            .Where(x => x.UserId == userId)
            .AsQueryable()
            .SingleOrDefaultAsync(x => x.Id == recordId);

        return query;
    }

    public Task<Record> GetByNameAsync(string exerciseName, Guid userId)
    {
        var query = _dbContext.Records
            .Include(x => x.Exercise)
            .Where(x => x.UserId == userId)
            .AsQueryable()
            .SingleOrDefaultAsync(x => x.Exercise.Name == exerciseName);

        return query;
    }

    public async Task<(List<Record> items, int totalItemCount)> GetAllForUserAsync(int pageSize, int pageNumber, Guid userId)
    {
        var baseQuery = _dbContext.Records
            .Include(x => x.Exercise)
            .Where(x => x.UserId == userId)            
            .AsQueryable();

        var totalItemsCount = baseQuery.Count();

        var resultQuery = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();

        return (resultQuery, totalItemsCount);
    }

    public async Task AddAsync(Record record)
    {
        await _dbContext.Records.AddAsync(record);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddInRangeAsync(IList<Record> records)
    {
        await _dbContext.Records.AddRangeAsync(records);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Record record)
    {
        _dbContext.Records.Update(record);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Record record)
    {
        _dbContext.Records.Remove(record);
        await _dbContext.SaveChangesAsync();
    }
}
