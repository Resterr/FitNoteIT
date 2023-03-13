using FitNoteIT.Modules.Exercises.Core.Entities;

namespace FitNoteIT.Modules.Exercises.Core.Abstractions.Repositories;
internal interface IExerciseRepository
{
    Task<Exercise> GetByIdAsync(Guid id);
    Task<Exercise> GetByNameAsync(string name);
    Task<(List<Exercise> items, int totalItemCount)> PaginatedGetAllAsync(int pageSize, int pageNumber);
    Task<List<Exercise>> GetAllAsync();
    Task AddAsync(Exercise exercise);
    Task UpdateAsync(Exercise exercise);
    Task DeleteAsync(Exercise exercise);
}
