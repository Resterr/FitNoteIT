using AutoMapper;
using FitNoteIT.Modules.Users.Core.Abstractions;
using FitNoteIT.Modules.Users.Shared.DTO;
using FitNoteIT.Shared.Queries;

namespace FitNoteIT.Modules.Users.Core.Features.AdminFeature.Queries;
public record GetRolesForUser(Guid Id) : IQuery<List<RoleDto>>;

internal sealed class GetRolesForHandler : IQueryHandler<GetRolesForUser, List<RoleDto>>
{
	private readonly IUserRepository _userRepository;
	private readonly IMapper _mapper;

	public GetRolesForHandler(IUserRepository userRepository, IMapper mapper)
	{
		_userRepository = userRepository;
		_mapper = mapper;
	}
	public async Task<List<RoleDto>> HandleAsync(GetRolesForUser query, CancellationToken cancellationToken)
	{
		var user = await _userRepository.GetByIdAsync(query.Id);
		var result = _mapper.Map<List<RoleDto>>(user.Roles);
		
		return result;
	}
}