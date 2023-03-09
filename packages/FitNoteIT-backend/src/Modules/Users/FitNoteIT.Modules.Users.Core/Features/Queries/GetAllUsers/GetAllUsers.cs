using MediatR;
using AutoMapper;
using FitNoteIT.Shared.DTO;
using FitNoteIT.Modules.Users.Core.Abstractions.Repositories;
using FitNoteIT.Modules.Users.Shared.DTO;

namespace FitNoteIT.Modules.Users.Core.Features.Queries.GetAllUsers;
public record GetAllUsers(int PageSize = 0, int PageNumber = 0) : IRequest<PagedResultDto<UserDto>>;

internal sealed class GetAllUsersHandler : IRequestHandler<GetAllUsers, PagedResultDto<UserDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetAllUsersHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<PagedResultDto<UserDto>> Handle(GetAllUsers request, CancellationToken cancellationToken)
    {
        var (items, totalItemCount) = await _userRepository.GetAllAsync(request.PageSize, request.PageNumber);

        var result = new PagedResultDto<UserDto>(_mapper.Map<List<UserDto>>(items), totalItemCount, request.PageSize, request.PageNumber);

        return result;
    }
}
