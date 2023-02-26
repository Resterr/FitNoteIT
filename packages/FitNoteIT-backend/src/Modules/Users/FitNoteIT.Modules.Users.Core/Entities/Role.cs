namespace FitNoteIT.Modules.Users.Core.Entities;
public class Role
{
	public int Id { get; set; }
	public string Name { get; set; }

    private Role() { }

    internal Role(string name)
	{
		Name = name;
	}
}
