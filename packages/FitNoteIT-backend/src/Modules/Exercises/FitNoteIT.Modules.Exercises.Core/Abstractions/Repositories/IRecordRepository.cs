using FitNoteIT.Modules.Exercises.Core.Entities;

namespace FitNoteIT.Modules.Exercises.Core.Abstractions.Repositories;
internal interface IRecordRepository
{
    Task<Record> GetByIdAsync(Guid recordId, Guid userId);
    Task<Record> GetByNameAsync(string exerciseName, Guid userId);
    Task<(List<Record> items, int totalItemCount)> PaginatedGetAllForUserAsync(int pageSize, int pageNumber, Guid userId);
    Task<List<Record>> GetAllForUserAsync(Guid userId);
    Task AddAsync(Record record);
    Task AddInRangeAsync(IList<Record> records);
    Task UpdateAsync(Record record);
    Task DeleteAsync(Record record);

}
