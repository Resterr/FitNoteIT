using FitNoteIT.Modules.Exercises.Core.Entities;

namespace FitNoteIT.Modules.Exercises.Core.Abstractions.Services;
internal interface IRecordService
{
    Task<bool> RecordAlreadyExistsAsync(Guid userId, Exercise exercise);
}
