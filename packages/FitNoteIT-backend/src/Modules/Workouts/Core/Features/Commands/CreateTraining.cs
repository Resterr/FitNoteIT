using System.Globalization;
using FitNoteIT.Modules.Exercises.Shared;
using FitNoteIT.Modules.Users.Shared;
using FitNoteIT.Modules.Workouts.Core.Abstractions;
using FitNoteIT.Modules.Workouts.Core.Entities;
using FitNoteIT.Modules.Workouts.Shared.DTO;
using FitNoteIT.Shared.Commands;
using FitNoteIT.Shared.Exceptions;
using FitNoteIT.Shared.Services;
using FluentValidation;

namespace FitNoteIT.Modules.Workouts.Core.Features.Commands;

public record CreateTraining(string Date, List<TrainingDetailAddDto> Details) : ICommand;

internal sealed class CreateTrainingHandler : ICommandHandler<CreateTraining>
{
	private readonly ICurrentUserService _currentUserService;
	private readonly IExercisesModuleApi _exercisesModule;
	private readonly IWorkoutsMongoClient _mongoClient;
	private readonly IUsersModuleApi _usersModule;

	public CreateTrainingHandler(IWorkoutsMongoClient mongoClient, ICurrentUserService currentUserService, IUsersModuleApi usersModule, IExercisesModuleApi exercisesModule)
	{
		_mongoClient = mongoClient;
		_currentUserService = currentUserService;
		_usersModule = usersModule;
		_exercisesModule = exercisesModule;
	}

	public async Task HandleAsync(CreateTraining request, CancellationToken cancellationToken)
	{
		var userId = _currentUserService.UserId ?? throw new UnauthorizedAccessException();
		var user = await _usersModule.GetUserAsync(userId);
		var id = Guid.NewGuid();

		var exerciseIds = request.Details.Select(x => x.ExerciseId)
			.ToList();
		
		await _exercisesModule.GetExercises(exerciseIds);
		
		var format = "dd.MM.yyyy";
		
		if(!DateTime.TryParseExact(request.Date, format,  null, DateTimeStyles.None, out var trainingDate))
		{
			throw new InvalidDateFormat(format);
		}
		
		var trainingsDetails = new List<TrainingDetail>();
		foreach (var detail in request.Details)
		{
			var series = new List<SeriesDto>();
			if (detail.Series != null)
			{
				for (var i = 0; i < detail.Series.Count(); i++)
				{
					var newSerie = new SeriesDto() { Repeats = detail.Series[i][0], Weight = detail.Series[i][1] };
					series.Add(newSerie);
				}

				var newTrainingDetail = new TrainingDetail(detail.ExerciseId, series);
				trainingsDetails.Add(newTrainingDetail);
			}
		}
		
		var training = new Training(id, user.Id, DateOnly.FromDateTime(trainingDate), trainingsDetails);

		await _mongoClient.Trainings.InsertOneAsync(training);
	}
}

public class CreateTrainingValidator : AbstractValidator<CreateWorkoutPlan>
{
	public CreateTrainingValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty();
		RuleFor(x => x.Exercises)
			.NotEmpty();
	}
}