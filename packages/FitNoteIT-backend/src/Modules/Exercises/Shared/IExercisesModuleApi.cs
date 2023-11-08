using FitNoteIT.Modules.Exercises.Shared.DTO;

namespace FitNoteIT.Modules.Exercises.Shared;
public interface IExercisesModuleApi
{
    Task<ExerciseDto> GetExercise(Guid exerciseId);
    Task<List<ExerciseDto>> GetExercises(List<Guid> exercisesId);
}
