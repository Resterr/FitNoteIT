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

public record GetTrainingById(Guid Id) : IQuery<TrainingDto>;

internal sealed class GetTrainingByIdHandler : IQueryHandler<GetTrainingById, TrainingDto>
{
	private readonly ICurrentUserService _currentUserService;
	private readonly IExercisesModuleApi _exercisesModule;
	private readonly IWorkoutsMongoClient _mongoClient;
	private readonly IUsersModuleApi _usersModule;

	public GetTrainingByIdHandler(IWorkoutsMongoClient mongoClient, ICurrentUserService currentUserService, IUsersModuleApi usersModule, IExercisesModuleApi exercisesModule)
	{
		_mongoClient = mongoClient;
		_currentUserService = currentUserService;
		_usersModule = usersModule;
		_exercisesModule = exercisesModule;
	}

	public async Task<TrainingDto> HandleAsync(GetTrainingById request, CancellationToken cancellationToken)
	{
		var userId = _currentUserService.UserId ?? throw new UnauthorizedAccessException();
		var user = await _usersModule.GetUserAsync(userId);
		var filter = Builders<Training>.Filter.Eq(x => x.UserId, user.Id) & Builders<Training>.Filter.Eq(x => x.Id, request.Id);
		var training = await _mongoClient.Trainings.Find(filter)
			.FirstOrDefaultAsync() ?? throw new TrainingNotFound(request.Id);
		
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

		var result = new TrainingDto()
		{
			Id = training.Id,
			Date = training.Date,
			TrainingDetails = trainingDetails
		};
		
		return result;
	}
	
	public class GetTrainingByIdValidator : AbstractValidator<GetTrainingById>
	{
		public GetTrainingByIdValidator()
		{
			RuleFor(x => x.Id)
				.NotEmpty();
		}
	}
}