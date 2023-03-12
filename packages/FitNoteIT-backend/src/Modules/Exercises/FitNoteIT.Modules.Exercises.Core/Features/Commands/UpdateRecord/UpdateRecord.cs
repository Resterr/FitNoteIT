using FitNoteIT.Modules.Exercises.Core.Abstractions.Repositories;
using FitNoteIT.Modules.Exercises.Core.Abstractions.Services;
using FitNoteIT.Shared.Exceptions;
using FitNoteIT.Shared.Services;
using MediatR;

namespace FitNoteIT.Modules.Exercises.Core.Features.Commands.UpdateRecord;
public record UpdateRecord(string ExerciseName, double Result, DateTime RecordDate) : IRequest<Unit>;

internal sealed class UpdateRecordHandler : IRequestHandler<UpdateRecord, Unit>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IRecordReadService _recordReadService;
    private readonly IRecordRepository _recordRepository;

    public UpdateRecordHandler(ICurrentUserService currentUserService, IRecordReadService recordReadService, IRecordRepository recordRepository)
    {
        _currentUserService = currentUserService;
        _recordReadService = recordReadService;
        _recordRepository = recordRepository;
    }

    public async Task<Unit> Handle(UpdateRecord request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        if (await _recordReadService.IsUserExistsAsync(userId) == false) throw new BadRequestException("Invalid user");

        var record = await _recordRepository.GetByNameAsync(request.ExerciseName, (Guid)userId);

        if (record is null) throw new NotFoundException("Record not found");

        record.Update(request.Result, request.RecordDate.Date);

        await _recordRepository.UpdateAsync(record);

        return Unit.Value;
    }
}