using MediatR;
using FitNoteIT.Shared.Exceptions;
using AutoMapper;
using FitNoteIT.Modules.Users.Core.Common.DTO;
using FitNoteIT.Modules.Users.Core.Abstractions.Repositories;

namespace FitNoteIT.Modules.Users.Core.Features.Queries.GetUserById;
public record GetUserById(Guid Id) : IRequest<UserDto>;

internal sealed class GetUserByIdHandler : IRequestHandler<GetUserById, UserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserByIdHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    public async Task<UserDto> Handle(GetUserById request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);
        if (user is null) throw new NotFoundException("User not found");

        var result = _mapper.Map<UserDto>(user);

        return result;
    }
}
