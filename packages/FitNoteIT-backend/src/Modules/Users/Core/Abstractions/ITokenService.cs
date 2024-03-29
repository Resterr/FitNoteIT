﻿using System.Security.Claims;

namespace FitNoteIT.Modules.Users.Core.Abstractions;

public interface ITokenService
{
	string GenerateAccessToken(Guid userId, string userEmail, string userName, IEnumerable<string> roles);
	string GenerateAccessTokenFromClaims(IEnumerable<Claim> claims);
	string GenerateRefreshToken();
	ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
	DateTime GetRefreshExpiryDate();
}