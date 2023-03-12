using FitNoteIT.Modules.Users.Core.Abstractions.Factories;
using FitNoteIT.Modules.Users.Core.Entities;
using FitNoteIT.Modules.Users.Core.Security;
using FitNoteIT.Shared.Options;
using FitNoteIT.Shared.Time;
using Microsoft.Extensions.Configuration;

namespace FitNoteIT.Modules.Users.Core.Persistence.Seeders;
internal interface IUsersSeeder
{
    List<Role> SeedRoles();
    User SeedSuperAdmin(Role role);
}

internal sealed class UsersSeeder : IUsersSeeder
{
    private readonly IUserFactory _userFactory;
    private readonly IRoleFactory _roleFactory;
    private readonly IConfiguration _configuration;
    private readonly IPasswordManager _passwordManager;
    private readonly IClock _clock;

    public UsersSeeder(IUserFactory userFactory, IRoleFactory roleFactory, IConfiguration configuration, IPasswordManager passwordManager, IClock clock)
    {
        _userFactory = userFactory;
        _roleFactory = roleFactory;
        _configuration = configuration;
        _passwordManager = passwordManager;
        _clock = clock;
    }

    public List<Role> SeedRoles()
    {
        var roles = new List<Role>
        {
            _roleFactory.Create("SuperAdmin"),
            _roleFactory.Create("Admin"),
            _roleFactory.Create("User"),
        };
        return roles;
    }

    public User SeedSuperAdmin(Role role)
    {
        var superAdminConfig = _configuration.GetOptions<SuperAdminOptions>("SuperAdminAccount");
        var securedPassword = _passwordManager.Secure(superAdminConfig.Password);
        var superAdmin = _userFactory.Create(Guid.NewGuid(), superAdminConfig.Email, securedPassword, superAdminConfig.UserName, _clock.CurrentDate(), role);

        return superAdmin;
    }
}
