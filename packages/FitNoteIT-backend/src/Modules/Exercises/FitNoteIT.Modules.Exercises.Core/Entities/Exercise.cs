namespace FitNoteIT.Modules.Exercises.Core.Entities;
internal class Exercise
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }

    private Exercise() { }

    internal Exercise(Guid id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }

    internal void Update(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
