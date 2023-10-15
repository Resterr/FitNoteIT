using FitNoteIT.Modules.Users.Core.Abstractions;
using FitNoteIT.Modules.Users.Core.Exceptions;
using FitNoteIT.Modules.Users.Core.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FitNoteIT.Modules.Users.Core.Security;
internal sealed class AuthorizationService : IAuthorizationService
{
	private readonly UsersDbContext _dbContext;
	private readonly IUserRepository _userRepository;

	public AuthorizationService(UsersDbContext dbContext, IUserRepository userRepository)
	{
		_dbContext = dbContext;
		_userRepository = userRepository;
	}

	public async Task<bool> AuthenticateUserAsync(Guid userId)
	{
		return await _dbContext.Users.AnyAsync(x => x.Id == userId); ;
	}

	public async Task<bool> AuthorizeUserAsync(Guid userId, string roleName)
	{
		var user = await _dbContext.Users.Include(x => x.Roles).SingleOrDefaultAsync(x => x.Id == userId) ?? throw new UnauthorizedAccessException();
		return user.Roles.Select(x => x.Name).Contains(roleName);
	}

	public async Task AddUserToRoleAsync(Guid userId, string roleName)
	{
		var role = await _dbContext.Roles.SingleOrDefaultAsync(x => x.Name == roleName);
		if (role != null)
		{
			var user = await _userRepository.GetByIdAsync(userId);
			user.AddRole(role);
			await _userRepository.UpdateAsync(user);
		}
		else
		{
			throw new RoleNotFoundException(roleName);
		}
	}

	public async Task RemoveUserFromRoleAsync(Guid userId, string roleName)
	{
		var role = await _dbContext.Roles.SingleOrDefaultAsync(x => x.Name == roleName);
		if (role != null)
		{
			var user = await _userRepository.GetByIdAsync(userId);
			user.RemoveRole(role);
			await _userRepository.UpdateAsync(user);
		}
		else
		{
			throw new RoleNotFoundException(roleName);
		}
	}

	public async Task<IEnumerable<string>> GetRolesForUserAsync(Guid userId)
	{
		var user = await _userRepository.GetByIdAsync(userId);
		var roles = await _dbContext.Roles.Include(x => x.Users).Where(x => x.Users.Contains(user)).ToListAsync();

		return roles.Select(x => x.Name);
	}
}
