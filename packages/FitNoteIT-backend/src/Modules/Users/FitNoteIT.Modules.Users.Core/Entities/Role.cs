namespace FitNoteIT.Modules.Users.Core.Entities;
internal class Role
{
	public int Id { get; private set; }
	public string Name { get; private set; }

    private Role() { }

    internal Role(string name)
    {
        Name = name;
    }

    internal Role(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
