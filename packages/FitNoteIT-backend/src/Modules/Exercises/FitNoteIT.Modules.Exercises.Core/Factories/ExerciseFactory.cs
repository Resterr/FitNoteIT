using FitNoteIT.Modules.Exercises.Core.Abstractions.Factories;
using FitNoteIT.Modules.Exercises.Core.Entities;

namespace FitNoteIT.Modules.Exercises.Core.Factories;
internal sealed class ExerciseFactory : IExerciseFactory
{
    public Exercise Create(Guid id, string name, string description)
        => new(id, name, description);
}