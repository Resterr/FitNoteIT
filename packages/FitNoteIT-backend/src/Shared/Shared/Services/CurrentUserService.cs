using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace FitNoteIT.Shared.Services;

public interface ICurrentUserService
{
	ClaimsPrincipal? Principal { get; }
	Guid? UserId { get; }
}

internal sealed class CurrentUserService : ICurrentUserService
{
	private readonly IHttpContextAccessor _httpContextAccessor;

	public CurrentUserService(IHttpContextAccessor httpContextAccessor)
	{
		_httpContextAccessor = httpContextAccessor;
	}

	public ClaimsPrincipal? Principal => _httpContextAccessor.HttpContext?.User;

	public Guid? UserId => Principal?.FindFirstValue(ClaimTypes.NameIdentifier)
		.ToGuid();
}

internal static class Extensions
{
	public static Guid? ToGuid(this string? value)
	{
		return Guid.TryParse(value, out var result) ? result : null;
	}
}