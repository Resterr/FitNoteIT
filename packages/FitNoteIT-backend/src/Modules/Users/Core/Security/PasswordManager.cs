using FitNoteIT.Modules.Users.Core.Abstractions;
using FitNoteIT.Modules.Users.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace FitNoteIT.Modules.Users.Core.Security;

internal sealed class PasswordManager : IPasswordManager
{
	private const User _user = default;
	private readonly IPasswordHasher<User> _passwordHasher;

	public PasswordManager(IPasswordHasher<User> passwordHasher)
	{
		_passwordHasher = passwordHasher;
	}

	public string Secure(string password)
	{
		return _passwordHasher.HashPassword(_user, password);
	}

	public bool Validate(string password, string securedPassword)
	{
		return _passwordHasher.VerifyHashedPassword(_user, securedPassword, password) ==
			PasswordVerificationResult.Success;
	}
}