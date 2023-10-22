using AutoMapper;
using FitNoteIT.Modules.Users.Core.Abstractions;
using FitNoteIT.Modules.Users.Core.Exceptions;
using FitNoteIT.Modules.Users.Core.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FitNoteIT.Modules.Users.Core.Security;
internal sealed class AuthorizationService : IAuthorizationService
{
	private readonly UsersDbContext _dbContext;
	private readonly IUserRepository _userRepository;

	public AuthorizationService(UsersDbContext dbContext, IUserRepository userRepository, IMapper mapper)
	{
		_dbContext = dbContext;
		_userRepository = userRepository;
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
}
