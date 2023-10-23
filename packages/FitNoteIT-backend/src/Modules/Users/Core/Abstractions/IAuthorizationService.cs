namespace FitNoteIT.Modules.Users.Core.Abstractions;
public interface IAuthorizationService
{
	Task<bool> AuthorizeAsync(Guid userId, string roleName);
	Task AddUserToRoleAsync(Guid userId, string roleName);
	Task RemoveUserFromRoleAsync(Guid userId, string roleName);
}
