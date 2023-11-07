using FitNoteIT.Shared.Common;

namespace FitNoteIT.Modules.Users.Core.Entities;

public class Role : AuditableEntity
{
	public Guid Id { get; init; }
	public string Name { get; private set; }
	public List<User> Users { get; private set; } = new();

	private Role() { }

	internal Role(Guid id, string name)
	{
		Id = id;
		Name = name;
	}
}