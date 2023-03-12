using FitNoteIT.Modules.Exercises.Core.Abstractions.Repositories;
using FitNoteIT.Modules.Exercises.Core.Abstractions.Services;
using FitNoteIT.Shared.Exceptions;
using FitNoteIT.Shared.Services;
using MediatR;

namespace FitNoteIT.Modules.Exercises.Core.Features.Commands.UpdateRecord;
public record UpdateRecord(Guid Id, double Result, DateTime RecordDate) : IRequest<Unit>;

internal sealed class UpdateRecordHandler : IRequestHandler<UpdateRecord, Unit>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IRecordReadService _recordReadService;
    private readonly IRecordRepository _recordRepository;
    private readonly IExerciseRepository _exerciseRepository;

    public UpdateRecordHandler(ICurrentUserService currentUserService, IRecordReadService recordReadService, IRecordRepository recordRepository, IExerciseRepository exerciseRepository)
    {
        _currentUserService = currentUserService;
        _recordReadService = recordReadService;
        _recordRepository = recordRepository;
        _exerciseRepository = exerciseRepository;
    }

    public async Task<Unit> Handle(UpdateRecord request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        if (await _recordReadService.IsUserExistsAsync(userId) == false) throw new BadRequestException("Invalid user");

        var record = await _recordRepository.GetByIdAsync(request.Id, (Guid)userId);

        if (record is null) throw new NotFoundException("Record not found");

        record.Update(request.Result, request.RecordDate);

        await _recordRepository.UpdateAsync(record);

        return Unit.Value;
    }
}