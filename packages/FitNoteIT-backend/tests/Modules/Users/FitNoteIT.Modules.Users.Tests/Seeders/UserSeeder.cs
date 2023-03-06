using FitNoteIT.Modules.Users.Core.Abstractions.Factories;
using FitNoteIT.Modules.Users.Core.Entities;
using FitNoteIT.Modules.Users.Core.Factories;

namespace FitNoteIT.Modules.Users.Tests.Seeders;
internal class UserSeeder
{
    private readonly IUserFactory _userFactory;
    private readonly IRoleFactory _roleFactory;

    public UserSeeder()
    {
        _userFactory = new UserFactory();
        _roleFactory = new RoleFactory();
    }

    public User GetDefaultUser()
    {
        var user = _userFactory.Create(new Guid(), "Test@test.com", "Test", "Test", new DateTime(2023, 01, 01, 0, 0, 0), GetUserRole());
        return user;
    }

    private Role GetUserRole()
    {
        var role = _roleFactory.Create(1, "User");
        return role;
    }

}
