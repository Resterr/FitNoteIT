using AutoMapper;
using FitNoteIT.Modules.Exercises.Core.Abstractions.Factories;
using FitNoteIT.Modules.Exercises.Core.Abstractions.Repositories;
using FitNoteIT.Modules.Exercises.Core.Abstractions.Services;
using FitNoteIT.Modules.Exercises.Core.Entities;
using FitNoteIT.Modules.Exercises.Shared.DTO;
using FitNoteIT.Shared.DTO;
using FitNoteIT.Shared.Exceptions;
using FitNoteIT.Shared.Services;
using MediatR;

namespace FitNoteIT.Modules.Exercises.Core.Features.Queries.GetAllRecordsForUser;
public record GetAllRecordsForUser(int PageSize = 0, int PageNumber = 0) : IRequest<PagedResultDto<RecordDto>>;

internal sealed class GetAllRecordsForUserHandler : IRequestHandler<GetAllRecordsForUser, PagedResultDto<RecordDto>>
{
    private readonly IRecordRepository _recordRepository;
    private readonly IExerciseRepository _exerciseRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IRecordReadService _recordReadService;
    private readonly IRecordFactory _recordFactory;
    private readonly IMapper _mapper;

    public GetAllRecordsForUserHandler(IRecordRepository recordRepository, IExerciseRepository exerciseRepository, ICurrentUserService currentUserService, IRecordReadService recordReadService, IRecordFactory recordFactory, IMapper mapper)
    {
        _recordRepository = recordRepository;
        _exerciseRepository = exerciseRepository;
        _currentUserService = currentUserService;
        _recordReadService = recordReadService;
        _recordFactory = recordFactory;
        _mapper = mapper;
    }

    public async Task<PagedResultDto<RecordDto>> Handle(GetAllRecordsForUser request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        if (await _recordReadService.IsUserExistsAsync(userId) == false) throw new BadRequestException("Invalid user");

        if (await _recordReadService.IsRecordsAlreadyAdded((Guid)userId) == false)
        {
            var exercises = await _exerciseRepository.GetAllAsync();
            var currentRecords = await _recordRepository.GetAllForUserAsync((Guid)userId);
            var recordsToAdd = new List<Record>();

            foreach (var exercise in exercises)
            {
                if (!currentRecords.Select(x => x.Exercise.Id).Contains(exercise.Id))
                {
                    var record = _recordFactory.Create(Guid.NewGuid(), (Guid)userId, exercise.Id, null, null);                 
                    recordsToAdd.Add(record);
                }
            }

            await _recordRepository.AddInRangeAsync(recordsToAdd);
        }

        var (items, totalItemCount) = await _recordRepository.PaginatedGetAllForUserAsync(request.PageSize, request.PageNumber, (Guid)userId);
        var result = new PagedResultDto<RecordDto>(_mapper.Map<List<RecordDto>>(items), totalItemCount, request.PageSize, request.PageNumber);

        return result;
    }
}