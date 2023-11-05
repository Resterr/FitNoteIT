namespace FitNoteIT.Modules.Exercises.Core.Entities;

public class Exercise
{
	public Guid Id { get; init; }
	public Guid? CategoryId { get; }
	public Category? Category { get; private set; }
	public string Name { get; private set; }
	public string Description { get; private set; }
	public List<Record> Records { get; } = new();

	private Exercise() { }

	internal Exercise(Guid id, string name, string description, Category category)
	{
		Id = id;
		Name = name;
		Description = description;
		Category = category;
	}

	internal void Update(string? name, string? description, Category? category)
	{
		if (name != null) Name = name;
		if (description != null) Description = description;
		if (category != null) Category = category;
	}
}