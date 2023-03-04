using FluentValidation;

namespace FitNoteIT.Modules.Users.Core.Features.Commands.TokenRefresh;
public class RefreshTokenValidator : AbstractValidator<TokenRefresh>
{
    public RefreshTokenValidator()
    {
        RuleFor(x => x.AccessToken).NotEmpty();
        RuleFor(x => x.RefreshToken).NotEmpty();
    }
}
