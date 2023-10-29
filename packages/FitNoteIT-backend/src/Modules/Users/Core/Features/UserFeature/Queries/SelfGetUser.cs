using AutoMapper;
using FitNoteIT.Modules.Users.Core.Abstractions;
using FitNoteIT.Modules.Users.Shared.DTO;
using FitNoteIT.Shared.Queries;
using FitNoteIT.Shared.Services;

namespace FitNoteIT.Modules.Users.Core.Features.UserFeature.Queries;

public record SelfGetUser : IQuery<UserDto>;

internal sealed class SelfGetUserHandler : IQueryHandler<SelfGetUser, UserDto>
{
	private readonly ICurrentUserService _currentUserService;
	private readonly IMapper _mapper;
	private readonly IUserRepository _userRepository;

	public SelfGetUserHandler(IUserRepository userRepository, IMapper mapper, ICurrentUserService currentUserService)
	{
		_userRepository = userRepository;
		_mapper = mapper;
		_currentUserService = currentUserService;
	}

	public async Task<UserDto> HandleAsync(SelfGetUser request, CancellationToken cancellationToken)
	{
		var userId = _currentUserService.UserId ?? throw new UnauthorizedAccessException();
		var user = await _userRepository.GetByIdAsync(userId);
		var result = _mapper.Map<UserDto>(user);

		return result;
	}
}