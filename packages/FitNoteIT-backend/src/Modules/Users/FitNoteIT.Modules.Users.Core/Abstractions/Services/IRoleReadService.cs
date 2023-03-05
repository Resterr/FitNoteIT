using FitNoteIT.Modules.Users.Core.Entities;

namespace FitNoteIT.Modules.Users.Core.Abstractions.Services;
public interface IRoleReadService
{
    Task<Role> UserRoleAsync();
    Task<Role> AdminRoleAsync();
}
