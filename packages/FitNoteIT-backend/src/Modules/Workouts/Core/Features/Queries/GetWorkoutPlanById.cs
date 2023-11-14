using FitNoteIT.Modules.Exercises.Shared;
using FitNoteIT.Modules.Users.Shared;
using FitNoteIT.Modules.Workouts.Core.Abstractions;
using FitNoteIT.Modules.Workouts.Core.Entities;
using FitNoteIT.Modules.Workouts.Core.Exceptions;
using FitNoteIT.Modules.Workouts.Shared.DTO;
using FitNoteIT.Shared.Queries;
using FitNoteIT.Shared.Services;
using FluentValidation;
using MongoDB.Driver;

namespace FitNoteIT.Modules.Workouts.Core.Features.Queries;

public record GetWorkoutPlanById(Guid Id) : IQuery<WorkoutPlanDto>;

internal sealed class GetWorkoutPlanByIdHandler : IQueryHandler<GetWorkoutPlanById, WorkoutPlanDto>
{
	private readonly ICurrentUserService _currentUserService;
	private readonly IExercisesModuleApi _exercisesModule;
	private readonly IWorkoutsMongoClient _mongoClient;
	private readonly IUsersModuleApi _usersModule;

	public GetWorkoutPlanByIdHandler(IWorkoutsMongoClient mongoClient, ICurrentUserService currentUserService, IUsersModuleApi usersModule, IExercisesModuleApi exercisesModule)
	{
		_mongoClient = mongoClient;
		_currentUserService = currentUserService;
		_usersModule = usersModule;
		_exercisesModule = exercisesModule;
	}

	public async Task<WorkoutPlanDto> HandleAsync(GetWorkoutPlanById request, CancellationToken cancellationToken)
	{
		var userId = _currentUserService.UserId ?? throw new UnauthorizedAccessException();
		var user = await _usersModule.GetUserAsync(userId);
		var filter = Builders<WorkoutPlan>.Filter.Eq(x => x.UserId, user.Id) & Builders<WorkoutPlan>.Filter.Eq(x => x.Id, request.Id);
		var workoutPlan = await _mongoClient.WorkoutPlans.Find(filter)
				.FirstOrDefaultAsync() ??
			throw new WorkoutNotFound(request.Id);
		
		var exercises = await _exercisesModule.GetExercises(workoutPlan.Exercises);
		var result = new WorkoutPlanDto
		{
			Id = workoutPlan.Id,
			Name = workoutPlan.Name,
			Exercises = exercises
		};
		
		return result;
	}
	
	public class GetWorkoutPlanByIdValidator : AbstractValidator<GetWorkoutPlanById>
	{
		public GetWorkoutPlanByIdValidator()
		{
			RuleFor(x => x.Id)
				.NotEmpty();
		}
	}
}