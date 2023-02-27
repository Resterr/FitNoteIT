using FitNoteIT.Modules.Users.Core.Entities;
using FitNoteIT.Modules.Users.Core.Persistence.Contexts;
using FitNoteIT.Modules.Users.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace FitNoteIT.Modules.Users.Core.Persistence.Services;

internal sealed class RoleReadService : IRoleReadService
{
    private readonly UsersDbContext _dbContext;

    public RoleReadService(UsersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Role> UserRoleAsync()
    {
        return _dbContext.Roles
            .SingleOrDefaultAsync(x => x.Name == "User");
    }

    public Task<Role> AdminRoleAsync()
    {
        return _dbContext.Roles
            .SingleOrDefaultAsync(x => x.Name == "Admin");
    }
}
