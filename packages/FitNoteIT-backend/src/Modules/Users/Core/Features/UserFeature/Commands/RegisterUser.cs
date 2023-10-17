using FitNoteIT.Modules.Users.Core.Abstractions;
using FitNoteIT.Modules.Users.Core.Entities;
using FitNoteIT.Modules.Users.Core.Exceptions;
using FitNoteIT.Shared.Commands;
using FluentValidation;

namespace FitNoteIT.Modules.Users.Core.Features.UserFeature.Commands;
public record RegisterUser(string Email, string UserName, string Password, string ConfirmPassword) : ICommand
{
	public Guid Id { get; init; } = Guid.NewGuid();
}

internal sealed class RegisterUserHandler : ICommandHandler<RegisterUser>
{
	private readonly IAuthorizationService _authorizationService;
	private readonly IUserRepository _userRepository;
	private readonly IPasswordManager _passwordManager;

	public RegisterUserHandler(IAuthorizationService authorizationService, IUserRepository userRepository, IPasswordManager passwordManager)
	{
		_authorizationService = authorizationService;
		_userRepository = userRepository;
		_passwordManager = passwordManager;
	}

	public async Task HandleAsync(RegisterUser request, CancellationToken cancellationToken)
	{
		var email = request.Email;
		var userName = request.UserName;
		var password = request.Password;

		var available = await _userRepository.CredentialsAvailableForUser(email, userName);

		if (available) throw new InvalidUserCredentials();

		var hashedPassword = _passwordManager.Secure(password);
		var user = new User(request.Id, email, hashedPassword, userName);

		await _userRepository.AddAsync(user);
		await _authorizationService.AddUserToRoleAsync(user.Id, "User");
	}
}

public class RegisterUserValidator : AbstractValidator<RegisterUser>
{
	public RegisterUserValidator()
	{
		RuleFor(x => x.Email).NotNull().MinimumLength(6).MaximumLength(128).EmailAddress();
		RuleFor(x => x.UserName).NotNull().MinimumLength(3).MaximumLength(128);
		RuleFor(x => x.Password).NotNull().MinimumLength(6).MaximumLength(128);
		RuleFor(x => x.ConfirmPassword).NotNull().MinimumLength(6).MaximumLength(128).Equal(x => x.Password);
	}
}
