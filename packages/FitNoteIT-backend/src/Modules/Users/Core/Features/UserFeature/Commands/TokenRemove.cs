using FitNoteIT.Modules.Users.Core.Abstractions;
using FitNoteIT.Shared.Commands;
using FluentValidation;

namespace FitNoteIT.Modules.Users.Core.Features.UserFeature.Commands;

public record TokenRemove(Guid UserId) : ICommand;

internal sealed class TokenRemoveHandler : ICommandHandler<TokenRemove>
{
	private readonly IUserRepository _userRepository;

	public TokenRemoveHandler(IUserRepository userRepository)
	{
		_userRepository = userRepository;
	}

	public async Task HandleAsync(TokenRemove request, CancellationToken cancellationToken)
	{
		var user = await _userRepository.GetByIdAsync(request.UserId);
		user.RemoveRefreshToken();

		await _userRepository.UpdateAsync(user);
	}
}

public class TokenRemoveValidator : AbstractValidator<TokenRemove>
{
	public TokenRemoveValidator()
	{
		RuleFor(x => x.UserId)
			.NotEmpty();
	}
}