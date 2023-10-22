using FitNoteIT.Modules.Users.Core.Abstractions;
using FitNoteIT.Modules.Users.Core.Exceptions;
using FitNoteIT.Modules.Users.Shared.DTO;
using FitNoteIT.Shared.Queries;
using FluentValidation;

namespace FitNoteIT.Modules.Users.Core.Features.UserFeature.Queries;

public record LoginUser(string UserName, string Password) : IQuery<TokensDto>;

internal sealed class LoginUserHandler : IQueryHandler<LoginUser, TokensDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordManager _passwordManager;
	private readonly ITokenService _tokenService;

    public LoginUserHandler(IUserRepository userRepository, IPasswordManager passwordManager, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _passwordManager = passwordManager;
		_tokenService = tokenService;
    }

    public async Task<TokensDto> HandleAsync(LoginUser request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUserNameAsync(request.UserName);
        if (!_passwordManager.Validate(request.Password, user.PasswordHash)) throw new InvalidUserPassword();
		
        var accessToken = _tokenService.GenerateAccessToken(user.Id, user.Email, user.UserName, user.Roles.Select(x => x.Name));
        var refreshToken = _tokenService.GenerateRefreshToken();
        var refreshTokenExpiryDate = _tokenService.GetRefreshExpiryDate();

        user.SetRefreshToken(refreshToken);
        user.SetRefreshTokenExpiryTime(refreshTokenExpiryDate);

        await _userRepository.UpdateAsync(user);

        return new TokensDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }
}

public class LoginUserValidator : AbstractValidator<LoginUser>
{
	public LoginUserValidator()
	{
        RuleFor(x => x.UserName).NotNull();
        RuleFor(x => x.Password).NotNull();
	}
}