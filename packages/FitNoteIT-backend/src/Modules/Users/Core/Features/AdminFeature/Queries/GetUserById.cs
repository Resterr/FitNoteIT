using AutoMapper;
using FitNoteIT.Modules.Users.Core.Abstractions;
using FitNoteIT.Modules.Users.Core.Exceptions;
using FitNoteIT.Modules.Users.Shared.DTO;
using FitNoteIT.Shared.Queries;
using Microsoft.EntityFrameworkCore;

namespace FitNoteIT.Modules.Users.Core.Features.AdminFeature.Queries;

public record GetUserById(Guid Id) : IQuery<UserDto>;

internal sealed class GetUserByIdHandler : IQueryHandler<GetUserById, UserDto>
{
	private readonly IUsersDbContext _dbContext;
	private readonly IMapper _mapper;

	public GetUserByIdHandler(IUsersDbContext dbContext, IMapper mapper)
	{
		_dbContext = dbContext;
		_mapper = mapper;
	}

	public async Task<UserDto> HandleAsync(GetUserById request, CancellationToken cancellationToken)
	{
		var user = await _dbContext.Users.Include(x => x.Roles)
			.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken) ?? throw new UserNotFoundException(request.Id);
		var result = _mapper.Map<UserDto>(user);

		return result;
	}
}