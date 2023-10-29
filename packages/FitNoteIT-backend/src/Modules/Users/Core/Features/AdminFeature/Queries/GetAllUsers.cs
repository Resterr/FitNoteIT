using AutoMapper;
using FitNoteIT.Modules.Users.Core.Abstractions;
using FitNoteIT.Modules.Users.Shared.DTO;
using FitNoteIT.Shared.Queries;
using Microsoft.EntityFrameworkCore;

namespace FitNoteIT.Modules.Users.Core.Features.AdminFeature.Queries;

public record GetAllUsers : IQuery<List<UserDto>>;

internal sealed class GetAllUsersHandler : IQueryHandler<GetAllUsers, List<UserDto>>
{
	private readonly IUsersDbContext _dbContext;
	private readonly IMapper _mapper;

	public GetAllUsersHandler(IUsersDbContext dbContext, IMapper mapper)
	{
		_dbContext = dbContext;
		_mapper = mapper;
	}

	public async Task<List<UserDto>> HandleAsync(GetAllUsers request, CancellationToken cancellationToken)
	{
		var users = await _dbContext.Users.Include(x => x.Roles)
			.ToListAsync(cancellationToken: cancellationToken);
		var superAdmin = users.Where(user => user.Roles.Any(role => role.Name == "SuperAdmin"))
			.ToList();

		users.Remove(superAdmin[0]);
		
		var result = _mapper.Map<List<UserDto>>(users);

		return result;
	}
}