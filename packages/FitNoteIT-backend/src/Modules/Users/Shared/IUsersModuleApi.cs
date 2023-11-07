using FitNoteIT.Modules.Users.Shared.DTO;

namespace FitNoteIT.Modules.Users.Shared;

public interface IUsersModuleApi
{
	Task<UserDto> GetUserAsync(Guid userId);
}