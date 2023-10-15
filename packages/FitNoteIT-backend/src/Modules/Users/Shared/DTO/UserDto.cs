namespace FitNoteIT.Modules.Users.Shared.DTO;
public class UserDto
{ 
	public Guid Id { get; set; }
	public string Email { get; set; }
	public string UserName { get; set; }
    public string State { get; set; }
    public DateTime CreatedAt { get; set; }
}