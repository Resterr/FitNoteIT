using FitNoteIT.Modules.Users.Core.Abstractions;
using FitNoteIT.Modules.Users.Core.Exceptions;
using FitNoteIT.Shared.Commands;
using FitNoteIT.Shared.Exceptions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FitNoteIT.Modules.Users.Core.Features.AdminFeature.Commands;

public record RemoveRole(Guid Id, string RoleName) : ICommand;

internal sealed class RemoveRoleHandler : ICommandHandler<RemoveRole>
{
	private readonly IUsersDbContext _dbContext;

	public RemoveRoleHandler(IUsersDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task HandleAsync(RemoveRole request, CancellationToken cancellationToken)
	{
		if (request.RoleName.ToLower() == "superadmin") throw new AccessForbiddenException();

		var user = await _dbContext.Users.Include(x => x.Roles)
				.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken) ??
			throw new UserNotFoundException(request.Id);

		var roles = user.Roles.Select(x => x.Name)
			.ToList();

		if (roles.Contains("SuperAdmin")) throw new AccessForbiddenException();

		var role = await _dbContext.Roles.SingleOrDefaultAsync(x => x.Name == request.RoleName, cancellationToken);
		if (role != null)
		{
			var isRole = user.Roles.Select(x => x.Name.ToLower())
				.Contains(request.RoleName.ToLower());
			if (!isRole) throw new UserHasNoRoleException(user.Id, request.RoleName);
			user.RemoveRole(role);
		}
		else
		{
			throw new RoleNotFoundException(request.RoleName);
		}

		await _dbContext.SaveChangesAsync(cancellationToken);
	}
}

public class RemoveRoleValidator : AbstractValidator<GrantRole>
{
	public RemoveRoleValidator()
	{
		RuleFor(x => x.Id)
			.NotNull()
			.NotEmpty();
		RuleFor(x => x.RoleName)
			.NotNull()
			.NotEmpty();
	}
}