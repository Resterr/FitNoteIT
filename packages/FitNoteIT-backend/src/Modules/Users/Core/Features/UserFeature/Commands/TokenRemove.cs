using FitNoteIT.Modules.Users.Core.Abstractions;
using FitNoteIT.Modules.Users.Core.Exceptions;
using FitNoteIT.Shared.Commands;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FitNoteIT.Modules.Users.Core.Features.UserFeature.Commands;

public record TokenRemove(Guid UserId) : ICommand;

internal sealed class TokenRemoveHandler : ICommandHandler<TokenRemove>
{
	private readonly IUsersDbContext _dbContext;

	public TokenRemoveHandler(IUsersDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task HandleAsync(TokenRemove request, CancellationToken cancellationToken)
	{
		var user = await _dbContext.Users.Include(x => x.Roles)
				.SingleOrDefaultAsync(x => x.Id == request.UserId, cancellationToken) ??
			throw new UserNotFoundException(request.UserId);

		user.RemoveRefreshToken();

		_dbContext.Users.Update(user);
		await _dbContext.SaveChangesAsync(cancellationToken);
	}
}

public class TokenRemoveValidator : AbstractValidator<TokenRemove>
{
	public TokenRemoveValidator()
	{
		RuleFor(x => x.UserId)
			.NotNull()
			.NotEmpty();
	}
}