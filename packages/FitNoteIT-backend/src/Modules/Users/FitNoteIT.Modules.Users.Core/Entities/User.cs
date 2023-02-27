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
}
