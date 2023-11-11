using AutoMapper;
using FitNoteIT.Modules.Exercises.Shared;
using FitNoteIT.Modules.Users.Shared;
using FitNoteIT.Modules.Workouts.Core.Entities;
using FitNoteIT.Modules.Workouts.Core.Persistense.Clients;
using FitNoteIT.Modules.Workouts.Shared.DTO;
using FitNoteIT.Shared.Queries;
using FitNoteIT.Shared.Services;
using MongoDB.Driver;

namespace FitNoteIT.Modules.Workouts.Core.Features.Queries;

public record GetAllWorkoutPlansForUser : IQuery<List<WorkoutPlanDto>>;

internal sealed class GetAllWorkoutPlanForUserHandler : IQueryHandler<GetAllWorkoutPlansForUser, List<WorkoutPlanDto>>
{
	private readonly WorkoutsMongoClient _mongoClient;
	private readonly ICurrentUserService _currentUserService;
	private readonly IUsersModuleApi _usersModule;
	private readonly IExercisesModuleApi _exercisesModule;
	private readonly IMapper _mapper;

	public GetAllWorkoutPlanForUserHandler(WorkoutsMongoClient mongoClient, ICurrentUserService currentUserService, IUsersModuleApi usersModule, IExercisesModuleApi exercisesModule, IMapper mapper)
	{
		_mongoClient = mongoClient;
		_currentUserService = currentUserService;
		_usersModule = usersModule;
		_exercisesModule = exercisesModule;
		_mapper = mapper;
	}

	public async Task<List<WorkoutPlanDto>> HandleAsync(GetAllWorkoutPlansForUser request, CancellationToken cancellationToken)
	{
		var userId = _currentUserService.UserId ?? throw new UnauthorizedAccessException();
		var user = await _usersModule.GetUserAsync(userId);
		var filter = Builders<WorkoutPlan>.Filter.Eq(x => x.UserId, user.Id);
		var workoutPlans = await _mongoClient.WorkoutPlans.Find(filter)
			.ToListAsync();

		var result = new List<WorkoutPlanDto>();

		foreach (var workoutPlan in workoutPlans)
		{
			var exercises = await _exercisesModule.GetExercises(workoutPlan.Exercises);
			var newWorkoutPlanDto = new WorkoutPlanDto()
			{
				Id = workoutPlan.Id,
				Name = workoutPlan.Name,
				Exercises = exercises
			};

			result.Add(newWorkoutPlanDto);
		}
		
		return result;
	}
}