using FitNoteIT.Modules.Exercises.Shared.DTO;

namespace FitNoteIT.Modules.Workouts.Shared.DTO;
public class WorkoutPlanDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<ExerciseDto> Exercises { get; set; }
}
