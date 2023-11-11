using FitNoteIT.Modules.Exercises.Shared.DTO;

namespace FitNoteIT.Modules.Workouts.Shared.DTO;

public class TrainingDetailDto
{
	public ExerciseDto? Exercise { get; set; }
	public int Number { get; set; }
	public int Weight { get; set; }
}