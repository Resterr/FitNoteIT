using FitNoteIT.Modules.Users.Core.Abstractions;
using FitNoteIT.Modules.Users.Core.Entities;
using FitNoteIT.Modules.Users.Core.Options;
using FitNoteIT.Shared.Options;
using Microsoft.Extensions.Configuration;

namespace FitNoteIT.Modules.Users.Core.Persistence.Seeders;

internal interface IUsersSeeder
{
	User SeedSuperAdmin();
	List<Role> SeedDefaultRoles();
}

internal sealed class UsersSeeder : IUsersSeeder
{
	private readonly IConfiguration _configuration;
	private readonly IPasswordManager _passwordManager;

	public UsersSeeder(IConfiguration configuration, IPasswordManager passwordManager)
	{
		_configuration = configuration;
		_passwordManager = passwordManager;
	}


	public User SeedSuperAdmin()
	{
		var superAdminConfig = _configuration.GetOptions<SuperAdminOptions>("SuperAdminAccount");
		var securedPassword = _passwordManager.Secure(superAdminConfig.Password);
		var superAdmin = new User(Guid.NewGuid(), superAdminConfig.Email, securedPassword, superAdminConfig.UserName);

		return superAdmin;
	}

	public List<Role> SeedDefaultRoles()
	{
		var roles = new List<Role>
		{
			new(Guid.NewGuid(), "SuperAdmin"),
			new(Guid.NewGuid(), "Admin"),
			new(Guid.NewGuid(), "User")
		};

		return roles;
	}
}