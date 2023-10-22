using AutoMapper;
using FitNoteIT.Modules.Users.Core.Abstractions;
using FitNoteIT.Modules.Users.Core.Exceptions;
using FitNoteIT.Modules.Users.Core.Persistence;
using FitNoteIT.Shared.Exceptions;
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

	public async Task<bool> AuthorizeAsync(Guid userId, string roleName)
	{
		var user = await _dbContext.Users.Include(x => x.Roles)
			.SingleOrDefaultAsync(x => x.Id == userId);
		
		if (user == null)
		{
			throw new AccessForbiddenException();
		}

		if (user.Roles.Select(x => x.Name)
			.Contains(roleName))
		{
			return true;
		}
		else
		{
			throw new AccessForbiddenException();
		}
		
	}
	
	public async Task AddUserToRoleAsync(Guid userId, string roleName)
	{
		var role = await _dbContext.Roles.SingleOrDefaultAsync(x => x.Name == roleName);
		if (role != null)
		{
			var user = await _userRepository.GetByIdAsync(userId);
			var isRole = user.Roles.Select(x => x.Name.ToLower()).Contains(roleName.ToLower());
			if (isRole)
			{
				throw new UserHasRoleException(user.Id, roleName);
			}

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
			var isRole = user.Roles.Select(x => x.Name.ToLower()).Contains(roleName.ToLower());
			if (!isRole)
			{
				throw new UserHasNoRoleException(user.Id, roleName);
			}
			user.RemoveRole(role);
			await _userRepository.UpdateAsync(user);
		}
		else
		{
			throw new RoleNotFoundException(roleName);
		}
	}
}
