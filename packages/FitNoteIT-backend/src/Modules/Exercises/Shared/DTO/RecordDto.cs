namespace FitNoteIT.Modules.Exercises.Shared.DTO;

public class RecordDto
{
	public Guid Id { get; set; }
	public Guid ExerciseId { get; set; }
	public string? ExerciseName { get; set; }
	public int Result { get; set; }
	public DateTime RecordDate { get; set; }
}