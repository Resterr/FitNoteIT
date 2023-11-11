using System.Globalization;
using System.Text.RegularExpressions;
using AutoMapper;
using FitNoteIT.Modules.Exercises.Shared;
using FitNoteIT.Modules.Users.Shared;
using FitNoteIT.Modules.Workouts.Core.Abstractions;
using FitNoteIT.Modules.Workouts.Core.Entities;
using FitNoteIT.Modules.Workouts.Core.Exceptions;
using FitNoteIT.Modules.Workouts.Shared.DTO;
using FitNoteIT.Shared.Commands;
using FitNoteIT.Shared.Services;
using FluentValidation;

namespace FitNoteIT.Modules.Workouts.Core.Features.Commands;

public record CreateTraining(string Date, List<TrainingDetailAddDto> Details) : ICommand;

internal sealed class CreateTrainingHandler : ICommandHandler<CreateTraining>
{
	private readonly ICurrentUserService _currentUserService;
	private readonly IExercisesModuleApi _exercisesModule;
	private readonly IMapper _mapper;
	private readonly IWorkoutsMongoClient _mongoClient;
	private readonly IUsersModuleApi _usersModule;

	public CreateTrainingHandler(IWorkoutsMongoClient mongoClient, ICurrentUserService currentUserService, IUsersModuleApi usersModule, IExercisesModuleApi exercisesModule, IMapper mapper)
	{
		_mongoClient = mongoClient;
		_currentUserService = currentUserService;
		_usersModule = usersModule;
		_exercisesModule = exercisesModule;
		_mapper = mapper;
	}

	public async Task HandleAsync(CreateTraining request, CancellationToken cancellationToken)
	{
		var userId = _currentUserService.UserId ?? throw new UnauthorizedAccessException();
		var user = await _usersModule.GetUserAsync(userId);
		var id = Guid.NewGuid();

		var exerciseIds = request.Details.Select(x => x.ExerciseId)
			.ToList();
		
		await _exercisesModule.GetExercises(exerciseIds);

		var pattern = @"\([^()]*\)";
		var dateFormatted = Regex.Replace(request.Date, pattern, string.Empty)
			.Trim();
		var format = "ddd MMM dd yyyy HH:mm:ss 'GMT'zzz";
		var trainingDate = DateOnly.FromDateTime(DateTime.ParseExact(dateFormatted, format, CultureInfo.InvariantCulture));

		var trainingDetails = _mapper.Map<List<TrainingDetail>>(request.Details) ?? throw new InvalidTrainingDetailData();
		var training = new Training(id, user.Id, trainingDate, trainingDetails);

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