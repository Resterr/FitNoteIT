using FitNoteIT.Modules.Exercises.Core.Entities;

namespace FitNoteIT.Modules.Exercises.Core.Abstractions.Factories;
internal interface IExerciseFactory
{
    Exercise Create(Guid id, string name, string description);
}
