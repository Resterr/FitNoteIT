namespace FitNoteIT.Modules.Exercises.Shared.DTO;
public class RecordDto
{
    public Guid Id { get; set; }
    public string ExerciseName { get; set; }
    public double? Result { get; set; }
    public DateTime? RecordDate { get; set; }
}
