using System.Globalization;
using System.Text.RegularExpressions;
using FitNoteIT.Modules.Exercises.Core.Abstractions;
using FitNoteIT.Modules.Exercises.Core.Entities;
using FitNoteIT.Modules.Exercises.Core.Exceptions;
using FitNoteIT.Modules.Users.Shared;
using FitNoteIT.Shared.Commands;
using FitNoteIT.Shared.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FitNoteIT.Modules.Exercises.Core.Features.RecordFeature.Commands;

public record CreateOrUpdateRecordForCurrentUser(Guid ExerciseId, string RecordDate, int Result) : ICommand
{
	public Guid Id { get; init; } = Guid.NewGuid();
}

internal sealed class CreateOrUpdateRecordHandler : ICommandHandler<CreateOrUpdateRecordForCurrentUser>
{
	private readonly ICurrentUserService _currentUserService;
	private readonly IExercisesDbContext _dbContext;
	private readonly IUsersModuleApi _usersModuleApi;

	public CreateOrUpdateRecordHandler(IExercisesDbContext dbContext, ICurrentUserService currentUserService, IUsersModuleApi usersModuleApi)
	{
		_dbContext = dbContext;
		_currentUserService = currentUserService;
		_usersModuleApi = usersModuleApi;
	}

	public async Task HandleAsync(CreateOrUpdateRecordForCurrentUser request, CancellationToken cancellationToken)
	{
		var userId = _currentUserService.UserId ?? throw new UnauthorizedAccessException();
		var user = await _usersModuleApi.GetUserAsync(userId);
		var exercise = await _dbContext.Exercises.SingleOrDefaultAsync(x => x.Id == request.ExerciseId) ?? throw new ExerciseNotFoundException(request.ExerciseId);
		var record = await _dbContext.Records.Where(x => x.UserId == user.Id)
			.SingleOrDefaultAsync(x => x.ExerciseId == request.ExerciseId);

		var pattern = @"\([^()]*\)";
		var dateFormatted = Regex.Replace(request.RecordDate, pattern, string.Empty).Trim();
		var format = "ddd MMM dd yyyy HH:mm:ss 'GMT'zzz";
		var recordDate = DateTime.ParseExact(dateFormatted, format, CultureInfo.InvariantCulture).Date;
		
		if (record != null)
		{
			record.Update(request.Result, recordDate);
		}
		else
		{
			var newRecord = new Record(request.Id, userId, request.Result, recordDate, exercise);

			await _dbContext.Records.AddAsync(newRecord);
		}

		await _dbContext.SaveChangesAsync(cancellationToken);
	}
}

public class CreateOrUpdateRecordForCurrentUserValidator : AbstractValidator<CreateOrUpdateRecordForCurrentUser>
{
	public CreateOrUpdateRecordForCurrentUserValidator()
	{
		RuleFor(x => x.ExerciseId)
			.NotNull()
			.NotEmpty();

		RuleFor(x => x.Result)
			.NotNull()
			.NotEmpty();
	}
}