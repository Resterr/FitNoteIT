using FitNoteIT.Modules.Users.Core.Entities;

namespace FitNoteIT.Modules.Users.Core.Abstractions.Services;
internal interface IRoleReadService
{
    Task<Role> UserRoleAsync();
    Task<Role> AdminRoleAsync();
}
