using AutoMapper;
using FitNoteIT.Modules.Exercises.Core.Abstractions.Repositories;
using FitNoteIT.Modules.Exercises.Core.Abstractions.Services;
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
    private readonly ICurrentUserService _currentUserService;
    private readonly IRecordReadService _recordReadService;
    private readonly IMapper _mapper;

    public GetAllRecordsForUserHandler(IRecordRepository recordRepository, ICurrentUserService currentUserService, IRecordReadService recordReadService, IMapper mapper)
    {
        _recordRepository = recordRepository;
        _currentUserService = currentUserService;
        _recordReadService = recordReadService;
        _mapper = mapper;
    }

    public async Task<PagedResultDto<RecordDto>> Handle(GetAllRecordsForUser request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        if (await _recordReadService.IsUserExistsAsync(userId) == false) throw new BadRequestException("Invalid user");

        var (items, totalItemCount) = await _recordRepository.GetAllForUserAsync(request.PageSize, request.PageNumber, (Guid)userId);
        var result = new PagedResultDto<RecordDto>(_mapper.Map<List<RecordDto>>(items), totalItemCount, request.PageSize, request.PageNumber);

        return result;
    }
}