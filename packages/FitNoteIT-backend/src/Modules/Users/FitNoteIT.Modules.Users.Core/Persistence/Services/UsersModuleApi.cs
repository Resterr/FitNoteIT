using AutoMapper;
using FitNoteIT.Modules.Users.Core.Abstractions.Repositories;
using FitNoteIT.Modules.Users.Shared;
using FitNoteIT.Modules.Users.Shared.DTO;
using FitNoteIT.Shared.Exceptions;

namespace FitNoteIT.Modules.Users.Core.Persistence.Services;
internal sealed class UsersModuleApi : IUsersModuleApi
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UsersModuleApi(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserDto> GetUserAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null) throw new NotFoundException("User not found");

        var result = _mapper.Map<UserDto>(user);

        return result;
    }

    public async Task<UserDto> GetUserAsync(string userName)
    {
        var user = await _userRepository.GetByUserName(userName);

        if (user == null) throw new NotFoundException("User not found");

        var result = _mapper.Map<UserDto>(user);

        return result;
    }
}
