using FitNoteIT.Modules.Users.Core.Persistence.Contexts;
using FitNoteIT.Modules.Users.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace FitNoteIT.Modules.Users.Core.Persistence.Services;

internal sealed class UserReadService : IUserReadService
{
    private readonly UsersDbContext _dbContext;

    public UserReadService(UsersDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public Task<bool> ExistsByEmailAsync(string email)
    {
        return _dbContext.Users.AnyAsync(x => x.Email == email);
    }

    public Task<bool> ExistsByUserNameAsync(string userName)
    {
        return _dbContext.Users.AnyAsync(x => x.UserName == userName);
    }
}
