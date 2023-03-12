namespace FitNoteIT.Modules.Exercises.Core.Abstractions.Services;
internal interface IRecordReadService
{
    Task<bool> RecordAlreadyExistsAsync(Guid userId, Guid exerciseId);
    Task<bool> IsUserExistsAsync(Guid? userId);
}
