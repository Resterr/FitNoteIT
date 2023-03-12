using FitNoteIT.Modules.Exercises.Core.Abstractions.Factories;
using FitNoteIT.Modules.Exercises.Core.Entities;

namespace FitNoteIT.Modules.Exercises.Core.Factories;
internal sealed class RecordFactory : IRecordFactory
{
    public Record Create(Guid id, Guid userId, Exercise exercise, double result, DateTime recordDate)
        => new(id, userId, exercise, result, recordDate);
}