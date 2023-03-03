using FitNoteIT.Shared.Exceptions;

namespace FitNoteIT.Modules.Users.Core.Entities;
public class User
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string UserName { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? VerifiedAt { get; private set; }
    public string RefreshToken { get; private set; }
    public DateTime? RefreshTokenExpiryTime { get; private set; }
    public Role UserRole { get; private set; }

    private User() { }

    internal User(Guid id, string email, string passwordHash, string userName, DateTime createdAt, Role userRole)
    {
        Id = id;
        Email = email;
        PasswordHash = passwordHash;
        UserName = userName;
        CreatedAt = createdAt;
        UserRole = userRole;
    }

    internal void Verify(DateTime verifiedAt)
    {
        if (VerifiedAt.HasValue)
        {
            throw new BadRequestException("User already verified");
        }

        VerifiedAt = verifiedAt;
    }

    internal void SetRefreshToken(string token)
    {
        RefreshToken = token;
    }

    internal void SetRefreshTokenExpiryTime(DateTime tokenExpireTime)
    {
        RefreshTokenExpiryTime = tokenExpireTime;
    }

    internal bool IsTokenValid(string token, DateTime currentDate)
    {
        if (RefreshToken == token && RefreshTokenExpiryTime <= currentDate) return true;
        else return false;
    }

    internal void RemoveRefreshToken()
    {
        RefreshToken = null;
        RefreshTokenExpiryTime = null;
    }
}
