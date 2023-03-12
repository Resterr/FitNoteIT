using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FitNoteIT.Shared.Services;

public interface ICurrentUserService
{
    ClaimsPrincipal Principal { get; }
    Guid? UserId { get; }
    string UserRole { get; }
}

internal sealed class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public ClaimsPrincipal Principal => _httpContextAccessor.HttpContext?.User;

    public Guid? UserId => Guid.Parse(Principal?.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

    public string UserRole => Principal?.FindFirst(c => c.Type == ClaimTypes.Role).Value;
}
