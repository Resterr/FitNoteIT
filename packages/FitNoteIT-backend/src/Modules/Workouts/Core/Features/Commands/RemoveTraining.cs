using FitNoteIT.Modules.Users.Shared;
using FitNoteIT.Modules.Workouts.Core.Abstractions;
using FitNoteIT.Modules.Workouts.Core.Entities;
using FitNoteIT.Modules.Workouts.Core.Exceptions;
using FitNoteIT.Shared.Commands;
using FitNoteIT.Shared.Services;
using FluentValidation;
using MongoDB.Driver;

namespace FitNoteIT.Modules.Workouts.Core.Features.Commands;

public record RemoveTraining(Guid Id) : ICommand;

internal sealed class RemoveTrainingHandler : ICommandHandler<RemoveTraining>
{
	private readonly ICurrentUserService _currentUserService;
	private readonly IWorkoutsMongoClient _mongoClient;
	private readonly IUsersModuleApi _usersModule;

	public RemoveTrainingHandler(IWorkoutsMongoClient mongoClient, ICurrentUserService currentUserService, IUsersModuleApi usersModule)
	{
		_mongoClient = mongoClient;
		_currentUserService = currentUserService;
		_usersModule = usersModule;
	}

	public async Task HandleAsync(RemoveTraining request, CancellationToken cancellationToken)
	{
		var userId = _currentUserService.UserId ?? throw new UnauthorizedAccessException();
		var user = await _usersModule.GetUserAsync(userId);
		var filter = Builders<Training>.Filter.Eq(x => x.Id, request.Id) & Builders<Training>.Filter.Eq(x => x.UserId, user.Id);
		var training = await _mongoClient.Trainings.Find(filter)
				.FirstOrDefaultAsync() ??
			throw new TrainingNotFound(request.Id);
		var deleteFilter = Builders<WorkoutPlan>.Filter.Eq(x => x.Id, training.Id);

		await _mongoClient.WorkoutPlans.DeleteOneAsync(deleteFilter);
	}

	public class RemoveTrainingValidator : AbstractValidator<RemoveTraining>
	{
		public RemoveTrainingValidator()
		{
			RuleFor(x => x.Id)
				.NotEmpty();
		}
	}
}