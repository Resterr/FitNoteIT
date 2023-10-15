namespace FitNoteIT.Modules.Users.Core.Abstractions;
public interface IAuthorizationService
{
	Task<bool> AuthenticateUserAsync(Guid userId);
	Task<bool> AuthorizeUserAsync(Guid userId, string roleName);
	Task AddUserToRoleAsync(Guid userId, string roleName);
	Task RemoveUserFromRoleAsync(Guid userId, string roleName);
	Task<IEnumerable<string>> GetRolesForUserAsync(Guid userId);
}
