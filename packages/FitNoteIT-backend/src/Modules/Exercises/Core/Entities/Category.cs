namespace FitNoteIT.Modules.Exercises.Core.Entities;

public class Category
{
	public Guid Id { get; init; }
	public string Name { get; private set; }
	public List<Exercise> Exercises { get; } = new();

	private Category() { }

	internal Category(Guid id, string name)
	{
		Id = id;
		Name = name;
	}

	internal void Update(string? name)
	{
		if (name != null) Name = name;
	}
}