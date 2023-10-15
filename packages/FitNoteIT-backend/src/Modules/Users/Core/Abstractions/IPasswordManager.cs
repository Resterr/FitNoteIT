namespace FitNoteIT.Modules.Users.Core.Abstractions;
public interface IPasswordManager
{
	string Secure(string password);
	bool Validate(string password, string securedPassword);
}