using FitNoteIT.Modules.Exercises.Core.Abstractions;
using FitNoteIT.Modules.Exercises.Core.Exceptions;
using FitNoteIT.Modules.Users.Shared;
using FitNoteIT.Shared.Commands;
using FitNoteIT.Shared.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FitNoteIT.Modules.Exercises.Core.Features.RecordFeature.Commands;

public record RemoveRecordForCurrentUser(Guid ExerciseId) : ICommand;

internal sealed class RemoveRecordForCurrentUserHandler : ICommandHandler<RemoveRecordForCurrentUser>
{
	private readonly ICurrentUserService _currentUserService;
	private readonly IExercisesDbContext _dbContext;
	private readonly IUsersModuleApi _usersModuleApi;

	public RemoveRecordForCurrentUserHandler(IExercisesDbContext dbContext, ICurrentUserService currentUserService, IUsersModuleApi usersModuleApi)
	{
		_dbContext = dbContext;
		_currentUserService = currentUserService;
		_usersModuleApi = usersModuleApi;
	}

	public async Task HandleAsync(RemoveRecordForCurrentUser request, CancellationToken cancellationToken)
	{
		var userId = _currentUserService.UserId ?? throw new UnauthorizedAccessException();
		var user = await _usersModuleApi.GetUserAsync(userId);
		var exercise = await _dbContext.Exercises.SingleOrDefaultAsync(x => x.Id == request.ExerciseId) ?? throw new ExerciseNotFoundException(request.ExerciseId);
		var record = await _dbContext.Records.Where(x => x.UserId == user.Id)
				.SingleOrDefaultAsync(x => x.ExerciseId == exercise.Id) ??
			throw new RecordNotFoundException(exercise.Name);

		_dbContext.Records.Remove(record);
		await _dbContext.SaveChangesAsync(cancellationToken);
	}
}

public class RemoveRecordForCurrentUserValidator : AbstractValidator<RemoveRecordForCurrentUser>
{
	public RemoveRecordForCurrentUserValidator()
	{
		RuleFor(x => x.ExerciseId)
			.NotNull()
			.NotEmpty();
	}
}