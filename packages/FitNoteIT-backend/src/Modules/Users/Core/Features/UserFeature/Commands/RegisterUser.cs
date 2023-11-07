using FitNoteIT.Modules.Users.Core.Abstractions;
using FitNoteIT.Modules.Users.Core.Entities;
using FitNoteIT.Modules.Users.Core.Exceptions;
using FitNoteIT.Shared.Commands;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FitNoteIT.Modules.Users.Core.Features.UserFeature.Commands;

public record RegisterUser(string Email, string UserName, string Password, string ConfirmPassword) : ICommand
{
	public Guid Id { get; init; } = Guid.NewGuid();
}

internal sealed class RegisterUserHandler : ICommandHandler<RegisterUser>
{
	private readonly IUsersDbContext _dbContext;
	private readonly IPasswordManager _passwordManager;

	public RegisterUserHandler(IUsersDbContext dbContext, IPasswordManager passwordManager)
	{
		_dbContext = dbContext;
		_passwordManager = passwordManager;
	}

	public async Task HandleAsync(RegisterUser request, CancellationToken cancellationToken)
	{
		var email = request.Email;
		var userName = request.UserName;
		var password = request.Password;

		var credentialsNotAvailable = await _dbContext.Users.AnyAsync(x => x.Email == email || x.UserName == userName, cancellationToken);

		if (credentialsNotAvailable) throw new InvalidUserCredentials();

		var hashedPassword = _passwordManager.Secure(password);
		var user = new User(request.Id, email, hashedPassword, userName);

		var role = await _dbContext.Roles.SingleOrDefaultAsync(x => x.Name == "User", cancellationToken);
		if (role != null)
			user.AddRole(role);
		else
			throw new RoleNotFoundException("User");

		await _dbContext.Users.AddAsync(user, cancellationToken);
		await _dbContext.SaveChangesAsync(cancellationToken);
	}
}

public class RegisterUserValidator : AbstractValidator<RegisterUser>
{
	public RegisterUserValidator()
	{
		RuleFor(x => x.Email)
			.NotNull()
			.NotEmpty()
			.MinimumLength(6)
			.MaximumLength(128)
			.EmailAddress();
		RuleFor(x => x.UserName)
			.NotNull()
			.NotEmpty()
			.MinimumLength(3)
			.MaximumLength(128);
		RuleFor(x => x.Password)
			.NotNull()
			.NotEmpty()
			.MinimumLength(6)
			.MaximumLength(128);
		RuleFor(x => x.ConfirmPassword)
			.NotNull()
			.NotEmpty()
			.MinimumLength(6)
			.MaximumLength(128)
			.Equal(x => x.Password);
	}
}