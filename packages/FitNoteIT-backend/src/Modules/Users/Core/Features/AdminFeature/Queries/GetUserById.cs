using AutoMapper;
using FitNoteIT.Modules.Users.Core.Abstractions;
using FitNoteIT.Modules.Users.Shared.DTO;
using FitNoteIT.Shared.Queries;

namespace FitNoteIT.Modules.Users.Core.Features.AdminFeature.Queries;
public record GetUserById(Guid Id) : IQuery<UserDto>;

internal sealed class GetUserByIdHandler : IQueryHandler<GetUserById, UserDto>
{
	private readonly IUserRepository _userRepository;
	private readonly IMapper _mapper;

	public GetUserByIdHandler(IUserRepository userRepository, IMapper mapper)
	{
		_userRepository = userRepository;
		_mapper = mapper;
	}
	public async Task<UserDto> HandleAsync(GetUserById request, CancellationToken cancellationToken)
	{
		var user = await _userRepository.GetByIdAsync(request.Id);
		var result = _mapper.Map<UserDto>(user);

		return result;
	}
}
