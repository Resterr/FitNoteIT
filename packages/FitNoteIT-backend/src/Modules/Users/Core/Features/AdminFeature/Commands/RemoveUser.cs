using FitNoteIT.Modules.Users.Core.Abstractions;
using FitNoteIT.Shared.Commands;
using FitNoteIT.Shared.Exceptions;
using FitNoteIT.Shared.Services;
using FluentValidation;

namespace FitNoteIT.Modules.Users.Core.Features.AdminFeature.Commands;

public record RemoveUser(Guid Id) : ICommand;

internal sealed class RemoveUserHandler : ICommandHandler<RemoveUser>
{
	private readonly IAuthorizationService _authorizationService;
	private readonly ICurrentUserService _currentUserService;
	private readonly IUserRepository _userRepository;

	public RemoveUserHandler(
		IUserRepository userRepository,
		ICurrentUserService currentUserService,
		IAuthorizationService authorizationService)
	{
		_userRepository = userRepository;
		_currentUserService = currentUserService;
		_authorizationService = authorizationService;
	}

	public async Task HandleAsync(RemoveUser request, CancellationToken cancellationToken)
	{
		var user = await _userRepository.GetByIdAsync(request.Id);
		var roles = user.Roles.Select(x => x.Name.ToLower())
			.ToList();

		if (roles.Contains("superadmin")) throw new AccessForbiddenException();
		if (roles.Contains("admin"))
		{
			var userId = _currentUserService.UserId ?? throw new AccessForbiddenException();
			await _authorizationService.AuthorizeAsync(userId, "superadmin");
		}

		await _userRepository.DeleteAsync(user);
	}
}

public class RemoveUserValidator : AbstractValidator<RemoveUser>
{
	public RemoveUserValidator()
	{
		RuleFor(x => x.Id)
			.NotNull();
	}
}