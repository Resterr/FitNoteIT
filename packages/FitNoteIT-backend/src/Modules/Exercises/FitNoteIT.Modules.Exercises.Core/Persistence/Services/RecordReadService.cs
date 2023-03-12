using FitNoteIT.Modules.Exercises.Core.Abstractions.Repositories;
using FitNoteIT.Modules.Exercises.Core.Abstractions.Services;
using FitNoteIT.Modules.Users.Shared;

namespace FitNoteIT.Modules.Exercises.Core.Persistence.Services;
internal sealed class RecordReadService : IRecordReadService
{
    private readonly IUsersModuleApi _usersModule;
    private readonly IRecordRepository _recordRepository;
    private readonly IExerciseRepository _exerciseRepository;

    public RecordReadService(IUsersModuleApi usersModule, IRecordRepository recordRepository, IExerciseRepository exerciseRepository)
    {
        _usersModule = usersModule;
        _recordRepository = recordRepository;
        _exerciseRepository = exerciseRepository;
    }

    public async Task<bool> IsUserExistsAsync(Guid? id)
    {
        if (id == null) return false;

        if (await _usersModule.GetUserAsync((Guid)id) is null) return false;

        return true;
    }

    public async Task<bool> IsRecordsAlreadyAdded(Guid userId)
    {
        if ((await _recordRepository.GetAllForUserAsync(100, 1, userId)).totalItemCount == (await _exerciseRepository.GetAllAsync(100, 1)).totalItemCount) return true;

        return false;
    }
}
