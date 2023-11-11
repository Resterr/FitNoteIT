namespace FitNoteIT.Modules.Workouts.Shared.DTO;

public class TrainingDetailAddDto
{
	public Guid ExerciseId { get; set; }
	public List<List<int>>? Series { get; set; }
}