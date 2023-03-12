using FitNoteIT.Modules.Exercises.Core.Entities;

namespace FitNoteIT.Modules.Exercises.Core.Abstractions.Repositories;
internal interface IRecordRepository
{
    Task<Record> GetByIdAsync(Guid recordId, Guid userId);
    Task<(List<Record> items, int totalItemCount)> GetAllForUserAsync(int pageSize, int pageNumber, Guid userId);
    Task AddAsync(Record record);
    Task UpdateAsync(Record record);
    Task DeleteAsync(Record record);

}
