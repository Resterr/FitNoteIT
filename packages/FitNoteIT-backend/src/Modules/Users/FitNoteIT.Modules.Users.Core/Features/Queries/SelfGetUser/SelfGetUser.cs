using AutoMapper;
using FitNoteIT.Modules.Users.Core.Common.DTO;
using FitNoteIT.Modules.Users.Core.Repositories;
using FitNoteIT.Shared.Exceptions;
using FitNoteIT.Shared.Services;
using MediatR;

namespace FitNoteIT.Modules.Users.Core.Features.Queries.SelfGetUser;

public record SelfGetUser() : IRequest<UserDto>;

internal sealed class SelfGetUserHandler : IRequestHandler<SelfGetUser, UserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public SelfGetUserHandler(IUserRepository userRepository, IMapper mapper, ICurrentUserService currentUserService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }
    public async Task<UserDto> Handle(SelfGetUser request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        if (userId is null) throw new BadRequestException("Invalid user"); 

        var user = await _userRepository.GetByIdAsync((Guid)userId);

        if (user is null) throw new NotFoundException("User not found");

        var result = _mapper.Map<UserDto>(user);

        return result;
    }
}
