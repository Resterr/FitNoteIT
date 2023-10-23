using FitNoteIT.Modules.Users.Core.Abstractions;
using FitNoteIT.Shared.Commands;
using FitNoteIT.Shared.Exceptions;
using FluentValidation;

namespace FitNoteIT.Modules.Users.Core.Features.AdminFeature.Commands;
public record GrantRole(Guid Id, string RoleName) : ICommand;

internal sealed class GrantRoleHandler : ICommandHandler<GrantRole>
{
	private readonly IUserRepository _userRepository;
	private readonly IAuthorizationService _authorizationService;

	public GrantRoleHandler(IUserRepository userRepository, IAuthorizationService authorizationService)
	{
		_userRepository = userRepository;
		_authorizationService = authorizationService;
	}

	public async Task HandleAsync(GrantRole request, CancellationToken cancellationToken)
	{
		if (request.RoleName.ToLower() == "superadmin") throw new AccessForbiddenException();
		
		var user = await _userRepository.GetByIdAsync(request.Id);

		await _authorizationService.AddUserToRoleAsync(user.Id, request.RoleName);
	}
}

public class GrantRoleValidator : AbstractValidator<GrantRole>
{
	public GrantRoleValidator()
	{
		RuleFor(x => x.Id).NotNull();
		RuleFor(x => x.RoleName).NotNull();
	}
}