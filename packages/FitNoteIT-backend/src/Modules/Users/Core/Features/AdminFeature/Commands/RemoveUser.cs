using FitNoteIT.Modules.Users.Core.Abstractions;
using FitNoteIT.Modules.Users.Core.Exceptions;
using FitNoteIT.Shared.Commands;
using FitNoteIT.Shared.Exceptions;
using FitNoteIT.Shared.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FitNoteIT.Modules.Users.Core.Features.AdminFeature.Commands;

public record RemoveUser(Guid Id) : ICommand;

internal sealed class RemoveUserHandler : ICommandHandler<RemoveUser>
{
	private readonly ICurrentUserService _currentUserService;
	private readonly IUsersDbContext _dbContext;

	public RemoveUserHandler(IUsersDbContext dbContext, ICurrentUserService currentUserService)
	{
		_dbContext = dbContext;
		_currentUserService = currentUserService;
	}

	public async Task HandleAsync(RemoveUser request, CancellationToken cancellationToken)
	{
		var currentUserId = _currentUserService.UserId ?? throw new UnauthorizedAccessException();
		var isCurrentUserExists = await _dbContext.Users.AnyAsync(x => x.Id == currentUserId);
		if (!isCurrentUserExists) throw new UnauthorizedAccessException();
		
		var user = await _dbContext.Users.Include(x => x.Roles)
				.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken) ??
			throw new UserNotFoundException(request.Id);
		var roles = user.Roles.Select(x => x.Name.ToLower())
			.ToList();

		if (roles.Contains("superadmin")) throw new AccessForbiddenException();
		if (roles.Contains("admin"))
		{
			var userId = _currentUserService.UserId ?? throw new AccessForbiddenException();
			var currentUser = await _dbContext.Users.Include(x => x.Roles)
				.SingleOrDefaultAsync(x => x.Id == userId, cancellationToken);

			if (currentUser == null) throw new AccessForbiddenException();

			if (!currentUser.Roles.Select(x => x.Name)
				.Contains("SuperAdmin"))
				throw new AccessForbiddenException();
		}

		_dbContext.Users.Remove(user);
		await _dbContext.SaveChangesAsync(cancellationToken);
	}
}

public class RemoveUserValidator : AbstractValidator<RemoveUser>
{
	public RemoveUserValidator()
	{
		RuleFor(x => x.Id)
			.NotEmpty();
	}
}