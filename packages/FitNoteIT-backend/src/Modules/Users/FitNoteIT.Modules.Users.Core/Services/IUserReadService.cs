namespace FitNoteIT.Modules.Users.Core.Services;
public interface IUserReadService
{
	Task<bool> ExistsByEmailAsync(string email);
	Task<bool> ExistsByUserNameAsync(string userName);
}
