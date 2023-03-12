using FitNoteIT.Modules.Exercises.Core.Abstractions.Factories;
using FitNoteIT.Modules.Exercises.Core.Abstractions.Repositories;
using FitNoteIT.Modules.Exercises.Core.Abstractions.Services;
using FitNoteIT.Shared.Exceptions;
using FitNoteIT.Shared.Services;
using MediatR;

namespace FitNoteIT.Modules.Exercises.Core.Features.Commands.CreateRecord;
public record CreateRecord(Guid ExerciseId, double Result, DateTime RecordDate) : IRequest<Guid>;

internal sealed class CreateRecordHandler : IRequestHandler<CreateRecord, Guid>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IRecordReadService _recordReadService;
    private readonly IRecordRepository _recordRepository;
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IRecordFactory _recordFactory;

    public CreateRecordHandler(ICurrentUserService currentUserService, IRecordReadService recordReadService, IRecordRepository recordRepository, IExerciseRepository exerciseRepository, IRecordFactory recordFactory)
    {
        _currentUserService = currentUserService;
        _recordReadService = recordReadService;
        _recordRepository = recordRepository;
        _exerciseRepository = exerciseRepository;
        _recordFactory = recordFactory;
    }

    public async Task<Guid> Handle(CreateRecord request, CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();
        var userId = _currentUserService.UserId;

        if (await _recordReadService.IsUserExistsAsync(userId) == false) throw new BadRequestException("Invalid user");

        var exercise = await _exerciseRepository.GetByIdAsync(request.ExerciseId);

        if (exercise is null) throw new NotFoundException("Exercise not found");
        if (await _recordReadService.RecordAlreadyExistsAsync((Guid)userId, exercise.Id) == true) throw new BadRequestException("Record already added");

        var record = _recordFactory.Create(id, (Guid)userId, exercise, request.Result, request.RecordDate);

        await _recordRepository.AddAsync(record);

        return id;
    }
}
