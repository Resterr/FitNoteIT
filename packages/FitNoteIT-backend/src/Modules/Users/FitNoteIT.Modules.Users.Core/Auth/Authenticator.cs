using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using FitNoteIT.Shared.Exceptions;
using FitNoteIT.Shared.Time;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FitNoteIT.Modules.Users.Core.Auth;
public interface IAuthenticator
{
    string GenerateAccessToken(Guid userId, string userEmail, string userName, string userRole);
    string GenerateAccessTokenFromClaims(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
    DateTime GetRefreshExpiryDate();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}

internal sealed class Authenticator : IAuthenticator
{
    private readonly IClock _clock;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly string _signingKey;
    private readonly TimeSpan _accessTokenExpiry;
    private readonly TimeSpan _refreshTokenExpiry;
    private readonly SigningCredentials _signingCredentials;
    private readonly JwtSecurityTokenHandler _jwtSecurityToken = new();

    public Authenticator(IOptions<AuthOptions> options, IClock clock)
    {
        _clock = clock;
        _issuer = options.Value.Issuer;
        _audience = options.Value.Audience;
        _signingKey = options.Value.SigningKey;
        _accessTokenExpiry = options.Value.AccessTokenExpiry ?? TimeSpan.FromMinutes(10);
        _refreshTokenExpiry = options.Value.RefreshTokenExpiry ?? TimeSpan.FromHours(24);
        _signingCredentials = new SigningCredentials(new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_signingKey)),
                SecurityAlgorithms.HmacSha256);
    }

    public string GenerateAccessToken(Guid userId, string userEmail, string userName, string userRole)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, userId.ToString()),
            new(ClaimTypes.Email, userEmail),
            new(ClaimTypes.Name, userName),
            new(ClaimTypes.Role, userRole)
        };

        var now = _clock.CurrentDate();
        var expires = now.Add(_accessTokenExpiry);
        var jwt = new JwtSecurityToken(_issuer, _audience, claims, now, expires, _signingCredentials);
        var token = _jwtSecurityToken.WriteToken(jwt);

        return token;
    }

    public string GenerateAccessTokenFromClaims(IEnumerable<Claim> claims)
    {
        var now = _clock.CurrentDate();
        var expires = now.Add(_accessTokenExpiry);
        var jwt = new JwtSecurityToken(_issuer, _audience, claims, now, expires, _signingCredentials);
        var token = _jwtSecurityToken.WriteToken(jwt);

        return token;
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public DateTime GetRefreshExpiryDate()
    {
        var now = _clock.CurrentDate();
        var expires = now.Add(_refreshTokenExpiry);

        return expires;
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = _issuer,
            ValidAudience = _audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_signingKey)),
            ValidateIssuerSigningKey = true,
            ValidateLifetime = false
        };

        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new BadRequestException("Invalid token");
                
            return principal;
        }
        catch
        {
            throw new BadRequestException("Invalid token");
        }
    }
}
