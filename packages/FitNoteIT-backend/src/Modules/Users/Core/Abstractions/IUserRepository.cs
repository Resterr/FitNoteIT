using FitNoteIT.Modules.Users.Core.Entities;
using FitNoteIT.Shared.Models;

namespace FitNoteIT.Modules.Users.Core.Abstractions;
public interface IUserRepository
{
    Task<PaginatedList<User>> PaginatedGetAllAsync(int pageNumber, int pageSize);
    Task<User> GetByIdAsync(Guid id);
    Task<User> GetByEmailAsync(string email);
    Task<User> GetByUserNameAsync(string userName);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
    Task<bool> CredentialsAvailableForUser(string? email, string? userName);
}
