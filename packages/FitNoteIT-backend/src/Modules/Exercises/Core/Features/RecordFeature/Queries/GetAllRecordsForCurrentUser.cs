using AutoMapper;
using FitNoteIT.Modules.Exercises.Core.Abstractions;
using FitNoteIT.Modules.Exercises.Shared.DTO;
using FitNoteIT.Modules.Users.Shared;
using FitNoteIT.Shared.Queries;
using FitNoteIT.Shared.Services;
using Microsoft.EntityFrameworkCore;

namespace FitNoteIT.Modules.Exercises.Core.Features.RecordFeature.Queries;

public record GetAllRecordsForCurrentUser : IQuery<List<RecordDto>>;

internal sealed class GetAllRecordsForCurrentUserHandler : IQueryHandler<GetAllRecordsForCurrentUser, List<RecordDto>>
{
	private readonly ICurrentUserService _currentUserService;
	private readonly IExercisesDbContext _dbContext;
	private readonly IMapper _mapper;
	private readonly IUsersModuleApi _usersModuleApi;

	public GetAllRecordsForCurrentUserHandler(IExercisesDbContext dbContext, ICurrentUserService currentUserService, IMapper mapper, IUsersModuleApi usersModuleApi)
	{
		_dbContext = dbContext;
		_currentUserService = currentUserService;
		_mapper = mapper;
		_usersModuleApi = usersModuleApi;
	}

	public async Task<List<RecordDto>> HandleAsync(GetAllRecordsForCurrentUser request, CancellationToken cancellationToken)
	{
		var userId = _currentUserService.UserId ?? throw new UnauthorizedAccessException();
		var user = await _usersModuleApi.GetUserAsync(userId);
		var records = await _dbContext.Records.Include(x => x.Exercise)
			.Where(x => x.UserId == user.Id)
			.ToListAsync(cancellationToken);
		var result = _mapper.Map<List<RecordDto>>(records);

		return result;
	}
}