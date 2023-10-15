using FitNoteIT.Modules.Users.Core.Abstractions;
using FitNoteIT.Modules.Users.Core.Exceptions;
using FitNoteIT.Modules.Users.Shared.DTO;
using FitNoteIT.Shared.Queries;
using FitNoteIT.Shared.Services;

namespace FitNoteIT.Modules.Users.Core.Features.UserFeature.Queries;
public record TokenRefresh(string AccessToken, string RefreshToken) : IQuery<TokensDto>;

internal sealed class TokenRefreshHandler : IQueryHandler<TokenRefresh, TokensDto>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
	private readonly ICurrentUserService _currentUserService;
	private readonly IDateTimeService _dateTimeService;

    public TokenRefreshHandler(IUserRepository userRepository, ITokenService tokenService, ICurrentUserService currentUserService, IDateTimeService dateTimeService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
		_currentUserService = currentUserService;
		_dateTimeService = dateTimeService;
    }

    public async Task<TokensDto> HandleAsync(TokenRefresh request, CancellationToken cancellationToken)
    {
        string accessToken = request.AccessToken;
        string refreshToken = request.RefreshToken;

        var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);

        var userId = _currentUserService.UserId;
        var user = await _userRepository.GetByIdAsync((Guid)userId);

        if (!user.IsTokenValid(refreshToken, _dateTimeService.CurrentDate())) throw new InvalidTokenException();

        var newAccessToken = _tokenService.GenerateAccessTokenFromClaims(principal.Claims);
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        user.SetRefreshToken(newRefreshToken);

        await _userRepository.UpdateAsync(user);

        return new TokensDto
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        };
    }
}
