﻿using FitNoteIT.Modules.Users.Core.Abstractions.Repositories;
using FitNoteIT.Modules.Users.Core.Auth;
using FitNoteIT.Modules.Users.Shared.DTO;
using FitNoteIT.Shared.Exceptions;
using FitNoteIT.Shared.Time;
using MediatR;
using System.Security.Claims;

namespace FitNoteIT.Modules.Users.Core.Features.Commands.TokenRefresh;
public record TokenRefresh(string AccessToken, string RefreshToken) : IRequest<TokensDto>;

internal sealed class TokenRefreshHandler : IRequestHandler<TokenRefresh, TokensDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticator _authenticator;
    private readonly IClock _clock;

    public TokenRefreshHandler(IUserRepository userRepository, IAuthenticator authenticator, IClock clock)
    {
        _userRepository = userRepository;
        _authenticator = authenticator;
        _clock = clock;
    }

    public async Task<TokensDto> Handle(TokenRefresh request, CancellationToken cancellationToken)
    {
        string accessToken = request.AccessToken;
        string refreshToken = request.RefreshToken;

        var principal = _authenticator.GetPrincipalFromExpiredToken(accessToken);

        var userId = principal.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
        
        if(userId == null) throw new BadRequestException("Invalid client request");

        var user = await _userRepository.GetByIdAsync(Guid.Parse(userId));

        if (!user.IsTokenValid(refreshToken, _clock.CurrentDate())) throw new BadRequestException("Invalid client request");

        var newAccessToken = _authenticator.GenerateAccessTokenFromClaims(principal.Claims);
        var newRefreshToken = _authenticator.GenerateRefreshToken();

        user.SetRefreshToken(newRefreshToken);

        await _userRepository.UpdateAsync(user);

        return new TokensDto
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        };
    }
}
