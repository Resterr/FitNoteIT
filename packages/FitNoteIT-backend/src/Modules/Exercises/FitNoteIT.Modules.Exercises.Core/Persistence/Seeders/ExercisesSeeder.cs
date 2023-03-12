using FitNoteIT.Modules.Exercises.Core.Abstractions.Factories;
using FitNoteIT.Modules.Exercises.Core.Entities;

namespace FitNoteIT.Modules.Exercises.Core.Persistence.Seeders;

internal interface IExercisesSeeder
{
    List<Exercise> SeedExercises();
}

internal sealed class ExercisesSeeder : IExercisesSeeder
{
    private readonly IExerciseFactory _exerciseFactory;

    public ExercisesSeeder(IExerciseFactory exerciseFactory)
    {
        _exerciseFactory = exerciseFactory;
    }

    public List<Exercise> SeedExercises()
    {
        var exercises = new List<Exercise>
        {
            _exerciseFactory.Create(Guid.NewGuid(), "Martwy ciąg", null),
            _exerciseFactory.Create(Guid.NewGuid(), "Pompki", null),
            _exerciseFactory.Create(Guid.NewGuid(), "Przysiad", null),
            _exerciseFactory.Create(Guid.NewGuid(), "Wiosłowanie", null),
            _exerciseFactory.Create(Guid.NewGuid(), "Podciąganie", null),
            _exerciseFactory.Create(Guid.NewGuid(), "Rwanie", null),
            _exerciseFactory.Create(Guid.NewGuid(), "Uginanie na biceps", null),
            _exerciseFactory.Create(Guid.NewGuid(), "Dipy", null),
            _exerciseFactory.Create(Guid.NewGuid(), "OHP", null),
            _exerciseFactory.Create(Guid.NewGuid(), "Wyciskanie na ławce", null),
        };

        return exercises;
    }
}
