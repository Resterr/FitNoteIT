using System.Security.Claims;
using FitNoteIT.Modules.Users.Core.Abstractions;
using FitNoteIT.Modules.Users.Core.Exceptions;
using FitNoteIT.Modules.Users.Shared.DTO;
using FitNoteIT.Shared.Queries;
using FitNoteIT.Shared.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FitNoteIT.Modules.Users.Core.Features.UserFeature.Queries;

public record TokenRefresh(string AccessToken, string RefreshToken) : IQuery<TokensDto>;

internal sealed class TokenRefreshHandler : IQueryHandler<TokenRefresh, TokensDto>
{
	private readonly IDateTimeService _dateTimeService;
	private readonly IUsersDbContext _dbContext;
	private readonly ITokenService _tokenService;

	public TokenRefreshHandler(IUsersDbContext dbContext, ITokenService tokenService, IDateTimeService dateTimeService)
	{
		_dbContext = dbContext;
		_tokenService = tokenService;
		_dateTimeService = dateTimeService;
	}

	public async Task<TokensDto> HandleAsync(TokenRefresh request, CancellationToken cancellationToken)
	{
		var accessToken = request.AccessToken;
		var refreshToken = request.RefreshToken;

		var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);

		if (Guid.TryParse(principal.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
			;
		else
			throw new InvalidTokenException();

		var user = await _dbContext.Users.Include(x => x.Roles)
				.SingleOrDefaultAsync(x => x.Id == userId, cancellationToken) ??
			throw new UserNotFoundException(userId);

		if (!user.IsTokenValid(refreshToken, _dateTimeService.CurrentDate())) throw new InvalidTokenException();

		var newAccessToken = _tokenService.GenerateAccessTokenFromClaims(principal.Claims);
		var newRefreshToken = _tokenService.GenerateRefreshToken();

		user.SetRefreshToken(newRefreshToken);

		_dbContext.Users.Update(user);
		await _dbContext.SaveChangesAsync(cancellationToken);

		return new TokensDto
		{
			AccessToken = newAccessToken,
			RefreshToken = newRefreshToken
		};
	}
}

public class TokenRefreshValidator : AbstractValidator<TokenRefresh>
{
	public TokenRefreshValidator()
	{
		RuleFor(x => x.AccessToken)
			.NotNull();
		RuleFor(x => x.RefreshToken)
			.NotNull();
	}
}