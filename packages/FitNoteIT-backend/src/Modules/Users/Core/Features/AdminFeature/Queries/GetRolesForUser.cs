using AutoMapper;
using FitNoteIT.Modules.Users.Core.Abstractions;
using FitNoteIT.Modules.Users.Core.Exceptions;
using FitNoteIT.Modules.Users.Shared.DTO;
using FitNoteIT.Shared.Queries;
using Microsoft.EntityFrameworkCore;

namespace FitNoteIT.Modules.Users.Core.Features.AdminFeature.Queries;

public record GetRolesForUser(Guid Id) : IQuery<List<RoleDto>>;

internal sealed class GetRolesForHandler : IQueryHandler<GetRolesForUser, List<RoleDto>>
{
	private readonly IUsersDbContext _dbContext;
	private readonly IMapper _mapper;

	public GetRolesForHandler(IUsersDbContext dbContext, IMapper mapper)
	{
		_dbContext = dbContext;
		_mapper = mapper;
	}

	public async Task<List<RoleDto>> HandleAsync(GetRolesForUser request, CancellationToken cancellationToken)
	{
		var user = await _dbContext.Users.Include(x => x.Roles)
			.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken) ?? throw new UserNotFoundException(request.Id);
		
		var result = _mapper.Map<List<RoleDto>>(user.Roles);

		return result;
	}
}