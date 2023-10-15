using FitNoteIT.Modules.Users.Core.Abstractions;
using FitNoteIT.Shared.Queries;

namespace FitNoteIT.Modules.Users.Core.Features.AdminFeature.Queries;
public record GetRolesForUser(Guid Id) : IQuery<List<string>>;

internal sealed class GetUserByIdHandler : IQueryHandler<GetRolesForUser, List<string>>
{
	private readonly IAuthorizationService _authorizationService;

	public GetUserByIdHandler(IAuthorizationService authorizationService)
	{
		_authorizationService = authorizationService;
	}
	public async Task<List<string>> HandleAsync(GetRolesForUser query, CancellationToken cancellationToken)
	{
		var result = await _authorizationService.GetRolesForUserAsync(query.Id);

		return result.ToList();
	}
}