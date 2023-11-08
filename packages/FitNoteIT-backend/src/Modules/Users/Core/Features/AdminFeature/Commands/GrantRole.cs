using FitNoteIT.Modules.Users.Core.Abstractions;
using FitNoteIT.Modules.Users.Core.Exceptions;
using FitNoteIT.Shared.Commands;
using FitNoteIT.Shared.Exceptions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FitNoteIT.Modules.Users.Core.Features.AdminFeature.Commands;

public record GrantRole(Guid Id, string RoleName) : ICommand;

internal sealed class GrantRoleHandler : ICommandHandler<GrantRole>
{
	private readonly IUsersDbContext _dbContext;

	public GrantRoleHandler(IUsersDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task HandleAsync(GrantRole request, CancellationToken cancellationToken)
	{
		if (request.RoleName.ToLower() == "superadmin") throw new AccessForbiddenException();

		var user = await _dbContext.Users.Include(x => x.Roles)
				.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken) ??
			throw new UserNotFoundException(request.Id);

		var role = await _dbContext.Roles.SingleOrDefaultAsync(x => x.Name == request.RoleName, cancellationToken);
		if (role != null)
		{
			var isRole = user.Roles.Select(x => x.Name.ToLower())
				.Contains(request.RoleName.ToLower());
			if (isRole) throw new UserHasRoleException(user.Id, request.RoleName.ToLower());

			user.AddRole(role);
			_dbContext.Users.Update(user);
			await _dbContext.SaveChangesAsync(cancellationToken);
		}
		else
		{
			throw new RoleNotFoundException(request.RoleName);
		}

		await _dbContext.SaveChangesAsync(cancellationToken);
	}
}

public class GrantRoleValidator : AbstractValidator<GrantRole>
{
	public GrantRoleValidator()
	{
		RuleFor(x => x.Id)
			.NotEmpty();
		
		RuleFor(x => x.RoleName)
			.NotEmpty();
	}
}