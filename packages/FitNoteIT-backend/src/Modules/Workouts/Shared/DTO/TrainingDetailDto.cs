using FitNoteIT.Modules.Exercises.Shared.DTO;

namespace FitNoteIT.Modules.Workouts.Shared.DTO;

public class TrainingDetailDto
{
	public ExerciseDto? Exercise { get; set; }
	public List<SeriesDto>? Series { get; set; }
}