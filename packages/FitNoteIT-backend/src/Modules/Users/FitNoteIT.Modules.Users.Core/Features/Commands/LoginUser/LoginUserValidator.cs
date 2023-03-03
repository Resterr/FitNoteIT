using FluentValidation;

namespace FitNoteIT.Modules.Users.Core.Features.Commands.LoginUser;
public class LoginUserValidator : AbstractValidator<LoginUser>
{
	public LoginUserValidator()
	{
		RuleFor(x => x.UserName).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}
