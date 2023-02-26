using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FitNoteIT.Shared.Services;

public interface ICurrentUserService
{
    Guid? GetUserId { get; }
    ClaimsPrincipal User { get; }
}

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public ClaimsPrincipal User => _httpContextAccessor.HttpContext?.User;

    public Guid? GetUserId => User is null ? null : Guid.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
}
