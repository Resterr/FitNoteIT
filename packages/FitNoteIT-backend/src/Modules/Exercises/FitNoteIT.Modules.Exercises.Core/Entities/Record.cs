namespace FitNoteIT.Modules.Exercises.Core.Entities;
internal class Record
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid ExerciseId { get; private set; }
    public virtual Exercise Exercise { get; private set; }
    public double? Result { get; private set; }
    public DateTime? RecordDate { get; private set; }

    private Record() { }

    internal Record(Guid id, Guid userId, Guid exerciseId, double? result, DateTime? recordDate)
    {
        Id = id;
        UserId = userId;
        ExerciseId = exerciseId;
        Result = result;
        RecordDate = recordDate;
    }

    internal void Update(double result, DateTime recordDate)
    {
        Result = result;
        RecordDate = recordDate;
    }
}
