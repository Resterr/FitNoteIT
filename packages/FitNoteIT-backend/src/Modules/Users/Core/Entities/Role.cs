using FitNoteIT.Shared.Common;

namespace FitNoteIT.Modules.Users.Core.Entities;
public class Role : AuditableEntity
{
	public Guid Id { get; set; }
	public string Name { get; private set; }
	public List<User> Users { get; private set; } = new();

	private Role() { }
	public Role(Guid id, string name)
	{
		Id = id;
		Name = name;
	}
}
