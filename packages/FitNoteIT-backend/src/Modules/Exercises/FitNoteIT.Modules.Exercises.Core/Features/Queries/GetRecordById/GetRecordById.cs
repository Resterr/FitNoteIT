using AutoMapper;
using FitNoteIT.Modules.Exercises.Core.Abstractions.Repositories;
using FitNoteIT.Modules.Exercises.Core.Abstractions.Services;
using FitNoteIT.Modules.Exercises.Shared.DTO;
using FitNoteIT.Shared.Exceptions;
using FitNoteIT.Shared.Services;
using MediatR;

namespace FitNoteIT.Modules.Exercises.Core.Features.Queries.GerRecordById;
public record GetRecordById(Guid Id) : IRequest<RecordDto>;

internal sealed class GetRecordByIdHandler : IRequestHandler<GetRecordById, RecordDto>
{
    private readonly IRecordRepository _recordRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IRecordReadService _recordReadService;
    private readonly IMapper _mapper;

    public GetRecordByIdHandler(IRecordRepository recordRepository, ICurrentUserService currentUserService, IRecordReadService recordReadService, IMapper mapper)
    {
        _recordRepository = recordRepository;
        _currentUserService = currentUserService;
        _recordReadService = recordReadService;
        _mapper = mapper;
    }
    public async Task<RecordDto> Handle(GetRecordById request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        if (await _recordReadService.IsUserExistsAsync(userId) == false) throw new BadRequestException("Invalid user");

        var record = await _recordRepository.GetByIdAsync(request.Id, (Guid)userId);

        if(record == null) throw new NotFoundException("Record not found");

        var result = _mapper.Map<RecordDto>(record);

        return result;
    }
}