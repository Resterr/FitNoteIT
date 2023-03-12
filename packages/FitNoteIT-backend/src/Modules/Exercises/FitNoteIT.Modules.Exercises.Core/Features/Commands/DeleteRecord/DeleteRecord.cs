using FitNoteIT.Modules.Exercises.Core.Abstractions.Repositories;
using FitNoteIT.Modules.Exercises.Core.Abstractions.Services;
using FitNoteIT.Shared.Exceptions;
using FitNoteIT.Shared.Services;
using MediatR;

namespace FitNoteIT.Modules.Exercises.Core.Features.Commands.DeleteRecord;
public record DeleteRecord(Guid Id) : IRequest<Unit>;

internal sealed class DeleteRecordHandler : IRequestHandler<DeleteRecord, Unit>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IRecordReadService _recordReadService;
    private readonly IRecordRepository _recordRepository;

    public DeleteRecordHandler(ICurrentUserService currentUserService, IRecordReadService recordReadService, IRecordRepository recordRepository)
    {
        _currentUserService = currentUserService;
        _recordReadService = recordReadService;
        _recordRepository = recordRepository;
    }

    public async Task<Unit> Handle(DeleteRecord request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        if (await _recordReadService.IsUserExistsAsync(userId) == false) throw new BadRequestException("Invalid user");

        var record = await _recordRepository.GetByIdAsync(request.Id, (Guid)userId);

        if (record is null) throw new NotFoundException("Record not found");

        await _recordRepository.DeleteAsync(record);

        return Unit.Value;
    }
}