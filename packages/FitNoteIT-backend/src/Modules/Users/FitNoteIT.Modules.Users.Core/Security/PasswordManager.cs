using FitNoteIT.Modules.Users.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace FitNoteIT.Modules.Users.Core.Security;

internal interface IPasswordManager
{
    string Secure(string password);
    bool Validate(string password, string securedPassword);
}


internal sealed class PasswordManager : IPasswordManager
{
	private readonly IPasswordHasher<User> _passwordHasher;

	public PasswordManager(IPasswordHasher<User> passwordHasher)
	{
		_passwordHasher = passwordHasher;
	}

	public string Secure(string password) => _passwordHasher.HashPassword(default, password);

	public bool Validate(string password, string securedPassword)
		=> _passwordHasher.VerifyHashedPassword(default, securedPassword, password) == PasswordVerificationResult.Success;
}
