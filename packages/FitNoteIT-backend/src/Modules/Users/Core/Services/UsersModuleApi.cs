using AutoMapper;
using FitNoteIT.Modules.Users.Core.Abstractions;
using FitNoteIT.Modules.Users.Shared;
using FitNoteIT.Modules.Users.Shared.DTO;

namespace FitNoteIT.Modules.Users.Core.Services;

internal sealed class UsersModuleApi : IUsersModuleApi
{
	private readonly IMapper _mapper;
	private readonly IUserRepository _userRepository;

	public UsersModuleApi(IUserRepository userRepository, IMapper mapper)
	{
		_userRepository = userRepository;
		_mapper = mapper;
	}

	public async Task<UserDto> GetUserAsync(Guid userId)
	{
		return _mapper.Map<UserDto>(await _userRepository.GetByIdAsync(userId));
	}

	public async Task<UserDto> GetUserAsync(string email)
	{
		return _mapper.Map<UserDto>(await _userRepository.GetByEmailAsync(email));
	}
}