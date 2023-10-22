using AutoMapper;
using FitNoteIT.Modules.Users.Core.Abstractions;
using FitNoteIT.Modules.Users.Shared.DTO;
using FitNoteIT.Shared.Queries;

namespace FitNoteIT.Modules.Users.Core.Features.UserFeature.Queries;

public record GetAllUsers() : IQuery<List<UserDto>>;

internal sealed class GetAllUsersHandler : IQueryHandler<GetAllUsers, List<UserDto>>
{
	private readonly IUserRepository _userRepository;
	private readonly IMapper _mapper;

	public GetAllUsersHandler(IUserRepository userRepository, IMapper mapper)
	{
		_userRepository = userRepository;
		_mapper = mapper;
	}
	public async Task<List<UserDto>> HandleAsync(GetAllUsers request, CancellationToken cancellationToken)
	{
		var users = await _userRepository.GetAllAsync();
		var result = _mapper.Map<List<UserDto>>(users);
		
		return result;
	}
}