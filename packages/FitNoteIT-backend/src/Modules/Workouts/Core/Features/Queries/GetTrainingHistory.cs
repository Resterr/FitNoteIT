using FitNoteIT.Modules.Exercises.Shared;
using FitNoteIT.Modules.Users.Shared;
using FitNoteIT.Modules.Workouts.Core.Abstractions;
using FitNoteIT.Modules.Workouts.Core.Entities;
using FitNoteIT.Modules.Workouts.Shared.DTO;
using FitNoteIT.Shared.Queries;
using FitNoteIT.Shared.Services;
using MongoDB.Driver;

namespace FitNoteIT.Modules.Workouts.Core.Features.Queries;

public record GetTrainingHistory : IQuery<List<TrainingDto>>;

internal sealed class GetTrainingHistoryHandler : IQueryHandler<GetTrainingHistory, List<TrainingDto>>
{
	private readonly ICurrentUserService _currentUserService;
	private readonly IExercisesModuleApi _exercisesModule;
	private readonly IWorkoutsMongoClient _mongoClient;
	private readonly IUsersModuleApi _usersModule;

	public GetTrainingHistoryHandler(IWorkoutsMongoClient mongoClient, ICurrentUserService currentUserService, IUsersModuleApi usersModule, IExercisesModuleApi exercisesModule)
	{
		_mongoClient = mongoClient;
		_currentUserService = currentUserService;
		_usersModule = usersModule;
		_exercisesModule = exercisesModule;
	}

	public async Task<List<TrainingDto>> HandleAsync(GetTrainingHistory request, CancellationToken cancellationToken)
	{
		var userId = _currentUserService.UserId ?? throw new UnauthorizedAccessException();
		var user = await _usersModule.GetUserAsync(userId);
		var filter = Builders<Training>.Filter.Eq(x => x.UserId, user.Id);
		var trainings = await _mongoClient.Trainings.Find(filter)
			.ToListAsync();

		var result = new List<TrainingDto>();

		foreach (var training in trainings)
		{
			var trainingDetails = new List<TrainingDetailDto>();

			foreach (var trainingDetail in training.TrainingDetails)
			{
				var newTrainingDetailDto = new TrainingDetailDto()
				{
					Exercise = await _exercisesModule.GetExercise(trainingDetail.ExerciseId),
					Series = trainingDetail.Series
				};
				
				trainingDetails.Add(newTrainingDetailDto);
			}

			var newTrainingDto = new TrainingDto()
			{
				Id = training.Id,
				Date = training.Date,
				TrainingDetails = trainingDetails
			};
			
			result.Add(newTrainingDto);
		}

		return result;
	}
}