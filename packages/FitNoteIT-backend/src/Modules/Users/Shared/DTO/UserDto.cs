namespace FitNoteIT.Modules.Users.Shared.DTO;

public class UserDto
{
	public Guid Id { get; set; }
	public string Email { get; set; }
	public string UserName { get; set; }
	public List<RoleDto> Roles { get; set; }
}