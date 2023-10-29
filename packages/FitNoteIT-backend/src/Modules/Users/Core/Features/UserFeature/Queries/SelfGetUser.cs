using AutoMapper;
using FitNoteIT.Modules.Users.Core.Abstractions;
using FitNoteIT.Modules.Users.Core.Exceptions;
using FitNoteIT.Modules.Users.Shared.DTO;
using FitNoteIT.Shared.Queries;
using FitNoteIT.Shared.Services;
using Microsoft.EntityFrameworkCore;

namespace FitNoteIT.Modules.Users.Core.Features.UserFeature.Queries;

public record SelfGetUser : IQuery<UserDto>;

internal sealed class SelfGetUserHandler : IQueryHandler<SelfGetUser, UserDto>
{
	private readonly ICurrentUserService _currentUserService;
	private readonly IUsersDbContext _dbContext;
	private readonly IMapper _mapper;

	public SelfGetUserHandler(IUsersDbContext dbContext, IMapper mapper, ICurrentUserService currentUserService)
	{
		_dbContext = dbContext;
		_mapper = mapper;
		_currentUserService = currentUserService;
	}

	public async Task<UserDto> HandleAsync(SelfGetUser request, CancellationToken cancellationToken)
	{
		var userId = _currentUserService.UserId ?? throw new UnauthorizedAccessException();
		var user = await _dbContext.Users.Include(x => x.Roles)
			.SingleOrDefaultAsync(x => x.Id == userId, cancellationToken: cancellationToken) ?? throw new UserNotFoundException(userId);
		var result = _mapper.Map<UserDto>(user);

		return result;
	}
}