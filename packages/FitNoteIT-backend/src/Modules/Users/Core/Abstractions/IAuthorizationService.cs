namespace FitNoteIT.Modules.Users.Core.Abstractions;
public interface IAuthorizationService
{
	Task AddUserToRoleAsync(Guid userId, string roleName);
	Task RemoveUserFromRoleAsync(Guid userId, string roleName);
}
