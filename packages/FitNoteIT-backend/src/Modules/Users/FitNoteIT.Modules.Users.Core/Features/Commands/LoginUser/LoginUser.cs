using MediatR;
using FitNoteIT.Shared.Exceptions;
using FitNoteIT.Modules.Users.Core.Security;
using FitNoteIT.Modules.Users.Core.Auth;
using FitNoteIT.Modules.Users.Core.Common.DTO;
using System.Security.Claims;
using FitNoteIT.Modules.Users.Core.Abstractions.Repositories;

namespace FitNoteIT.Modules.Users.Core.Features.Commands.LoginUser;
public record LoginUser(string UserName, string Password) : IRequest<TokensDto>;

internal sealed class LoginUserHandler : IRequestHandler<LoginUser, TokensDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordManager _passwordManager;
    private readonly IAuthenticator _authenticator;

    public LoginUserHandler(IUserRepository userRepository, IPasswordManager passwordManager, IAuthenticator authenticator)
    {
        _userRepository = userRepository;
        _passwordManager = passwordManager;
        _authenticator = authenticator;;
    }

    public async Task<TokensDto> Handle(LoginUser request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUserName(request.UserName);

        if (user is null) throw new NotFoundException("User not found");

        if (!_passwordManager.Validate(request.Password, user.PasswordHash)) throw new BadRequestException("Invalid password");

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.UserRole.Name)
        };

        var accessToken = _authenticator.GenerateAccessToken(claims);
        var refreshToken = _authenticator.GenerateRefreshToken();
        var refreshTokenExpiryDate = _authenticator.GetRefreshExpiryDate();

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
