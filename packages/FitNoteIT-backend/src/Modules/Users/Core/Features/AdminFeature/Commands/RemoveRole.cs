using FitNoteIT.Modules.Users.Core.Abstractions;
using FitNoteIT.Shared.Commands;
using FitNoteIT.Shared.Exceptions;
using FluentValidation;

namespace FitNoteIT.Modules.Users.Core.Features.AdminFeature.Commands;

public record RemoveRole(Guid Id, string RoleName) : ICommand;

internal sealed class RemoveRoleHandler : ICommandHandler<RemoveRole>
{
	private readonly IAuthorizationService _authorizationService;
	private readonly IUserRepository _userRepository;

	public RemoveRoleHandler(IUserRepository userRepository, IAuthorizationService authorizationService)
	{
		_userRepository = userRepository;
		_authorizationService = authorizationService;
	}

	public async Task HandleAsync(RemoveRole request, CancellationToken cancellationToken)
	{
		if (request.RoleName.ToLower() == "superadmin") throw new AccessForbiddenException();

		var user = await _userRepository.GetByIdAsync(request.Id);
		var roles = user.Roles.Select(x => x.Name)
			.ToList();

		if (roles.Contains("SuperAdmin")) throw new AccessForbiddenException();

		await _authorizationService.RemoveUserFromRoleAsync(user.Id, request.RoleName);
	}
}

public class RemoveRoleValidator : AbstractValidator<GrantRole>
{
	public RemoveRoleValidator()
	{
		RuleFor(x => x.Id)
			.NotNull();
		RuleFor(x => x.RoleName)
			.NotNull();
	}
}