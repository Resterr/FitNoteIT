using FitNoteIT.Modules.Users.Core.Entities;

namespace FitNoteIT.Modules.Users.Core.Repositories;

public interface IUserRepository
{
    Task<User> GetByIdAsync(Guid id);
    Task<User> GetByEmailAsync(string email);
    Task<(List<User> items, int totalItemCount)> GetAllAsync(int pageSize, int pageNumber);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
}
