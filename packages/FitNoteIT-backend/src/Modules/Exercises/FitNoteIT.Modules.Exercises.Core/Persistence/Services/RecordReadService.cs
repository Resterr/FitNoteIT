using FitNoteIT.Modules.Exercises.Core.Abstractions.Repositories;
using FitNoteIT.Modules.Exercises.Core.Abstractions.Services;
using FitNoteIT.Modules.Exercises.Core.Persistence.Contexts;
using FitNoteIT.Modules.Users.Shared;

namespace FitNoteIT.Modules.Exercises.Core.Persistence.Services;
internal sealed class RecordReadService : IRecordReadService
{
    private readonly ExercisesDbContext _dbContext;
    private readonly IUsersModuleApi _usersModule;

    public RecordReadService(ExercisesDbContext dbContext, IUsersModuleApi usersModule)
    {
        _dbContext = dbContext;
        _usersModule = usersModule;
    }

    public async Task<bool> IsUserExistsAsync(Guid? id)
    {
        if (id == null) return false;

        if (await _usersModule.GetUserAsync((Guid)id) is null) return false;

        return true;
    }

    public async Task<bool> IsRecordsAlreadyAdded(Guid userId)
    {
        await Task.CompletedTask;

        var exerciseCount = _dbContext.Exercises.Count();
        var userRecordsCount = _dbContext.Records.Where(x => x.UserId == userId).Count();

        if (exerciseCount == userRecordsCount) return true;

        return false;     
    }
}
