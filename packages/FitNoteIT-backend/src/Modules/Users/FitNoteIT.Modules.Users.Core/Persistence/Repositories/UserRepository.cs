using FitNoteIT.Modules.Users.Core.Abstractions.Repositories;
using FitNoteIT.Modules.Users.Core.Entities;
using FitNoteIT.Modules.Users.Core.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FitNoteIT.Modules.Users.Core.Persistence.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private readonly UsersDbContext _dbContext;

    public UserRepository(UsersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<User> GetByIdAsync(Guid id)
    {
        var query = _dbContext.Users
            .Include(x => x.UserRole)
            .SingleOrDefaultAsync(x => x.Id == id);

        return query;
    }

    public Task<User> GetByEmailAsync(string email)
    {
        var query = _dbContext.Users
            .Include(x => x.UserRole)
            .SingleOrDefaultAsync(x => x.Email == email);

        return query;
    }

    public Task<User> GetByUserName(string userName)
    {
        var query = _dbContext.Users
            .Include(x => x.UserRole)
            .SingleOrDefaultAsync(x => x.UserName == userName);

        return query;
    }

    public async Task<(List<User> items, int totalItemCount)> GetAllAsync(int pageSize, int pageNumber)
    {
        var baseQuery = _dbContext.Users
            .Include(x => x.UserRole)
            .AsQueryable();

        var totalItemsCount = baseQuery.Count();

        var resultQuery = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();

        return (resultQuery, totalItemsCount);
    }

    public async Task AddAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(User user)
    {
        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();
    }
}
