using AutoMapper;
using FitNoteIT.Modules.Users.Core.Abstractions;
using FitNoteIT.Modules.Users.Core.Exceptions;
using FitNoteIT.Modules.Users.Shared;
using FitNoteIT.Modules.Users.Shared.DTO;
using Microsoft.EntityFrameworkCore;

namespace FitNoteIT.Modules.Users.Core.Services;

internal sealed class UsersModuleApi : IUsersModuleApi
{
	private readonly IUsersDbContext _dbContext;
	private readonly IMapper _mapper;

	public UsersModuleApi(IUsersDbContext dbContext, IMapper mapper)
	{
		_dbContext = dbContext;
		_mapper = mapper;
	}

	public async Task<UserDto> GetUserAsync(Guid userId)
	{
		var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == userId) ?? throw new UserNotFoundException(userId);

		return _mapper.Map<UserDto>(user);
	}

	public async Task<UserDto> GetUserAsync(string email)
	{
		var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Email == email) ?? throw new UserNotFoundException(email, "email");

		return _mapper.Map<UserDto>(user);
	}
}