namespace FitNoteIT.Modules.Users.Core.Common.DTO;
public class UserDto
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string UserName { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? VerifiedAt { get; private set; }
}
