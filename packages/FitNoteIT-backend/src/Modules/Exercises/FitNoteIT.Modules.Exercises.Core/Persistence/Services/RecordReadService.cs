using FitNoteIT.Modules.Exercises.Core.Abstractions.Repositories;
using FitNoteIT.Modules.Exercises.Core.Abstractions.Services;
using FitNoteIT.Modules.Users.Shared;

namespace FitNoteIT.Modules.Exercises.Core.Persistence.Services;
internal sealed class RecordReadService : IRecordReadService
{
    private readonly IUsersModuleApi _usersModule;
    private readonly IRecordRepository _recordRepository;

    public RecordReadService(IUsersModuleApi usersModule, IRecordRepository recordRepository)
    {
        _usersModule = usersModule;
        _recordRepository = recordRepository;
    }

    public async Task<bool> IsUserExistsAsync(Guid? id)
    {
        if (id == null) return false;

        if (await _usersModule.GetUserAsync((Guid)id) is null) return false;

        return true;
    }

    public async Task<bool> RecordAlreadyExistsAsync(Guid userId, Guid exerciseId)
    {
        var userRecords = await _recordRepository.GetAllForUserAsync(100, 1, userId);

        if (userRecords.items.Any(x => x.Exercise.Id == exerciseId)) return true;

        return false;
    }
}
