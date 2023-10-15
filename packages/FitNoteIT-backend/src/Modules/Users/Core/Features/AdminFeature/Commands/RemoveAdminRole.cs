using FitNoteIT.Modules.Users.Core.Abstractions;
using FitNoteIT.Shared.Commands;

namespace FitNoteIT.Modules.Users.Core.Features.AdminFeature.Commands;
public record RemoveAdminRole(Guid Id) : ICommand;

internal sealed class RemoveAdminRoleHandler : ICommandHandler<RemoveAdminRole>
{
	private readonly IUserRepository _userRepository;
	private readonly IAuthorizationService _authorizationService;

	public RemoveAdminRoleHandler(IUserRepository userRepository, IAuthorizationService authorizationService)
	{
		_userRepository = userRepository;
		_authorizationService = authorizationService;
	}

	public async Task HandleAsync(RemoveAdminRole request, CancellationToken cancellationToken)
	{
		var user = await _userRepository.GetByIdAsync(request.Id);

		await _authorizationService.RemoveUserFromRoleAsync(user.Id, "Admin");
	}
}