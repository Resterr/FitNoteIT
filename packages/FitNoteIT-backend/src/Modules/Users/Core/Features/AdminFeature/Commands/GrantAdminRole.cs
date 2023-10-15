using FitNoteIT.Modules.Users.Core.Abstractions;
using FitNoteIT.Shared.Commands;

namespace FitNoteIT.Modules.Users.Core.Features.AdminFeature.Commands;
public record GrantAdminRole(Guid Id) : ICommand;

internal sealed class GrantAdminRoleHandler : ICommandHandler<GrantAdminRole>
{
	private readonly IUserRepository _userRepository;
	private readonly IAuthorizationService _authorizationService;

	public GrantAdminRoleHandler(IUserRepository userRepository, IAuthorizationService authorizationService)
	{
		_userRepository = userRepository;
		_authorizationService = authorizationService;
	}

	public async Task HandleAsync(GrantAdminRole command, CancellationToken cancellationToken)
	{
		var user = await _userRepository.GetByIdAsync(command.Id);

		await _authorizationService.AddUserToRoleAsync(user.Id, "Admin");
	}
}