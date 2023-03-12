namespace FitNoteIT.Modules.Exercises.Core.Abstractions.Services;
internal interface IRecordReadService
{
    Task<bool> IsRecordsAlreadyAdded(Guid userId);
    Task<bool> IsUserExistsAsync(Guid? userId);
}
