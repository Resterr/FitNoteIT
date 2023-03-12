using AutoMapper;
using FitNoteIT.Modules.Exercises.Core.Abstractions.Repositories;
using FitNoteIT.Modules.Exercises.Shared.DTO;
using FitNoteIT.Shared.DTO;
using MediatR;

namespace FitNoteIT.Modules.Exercises.Core.Features.Queries.GetAllExercises;
public record GetAllExercises(int PageSize = 0, int PageNumber = 0) : IRequest<PagedResultDto<ExerciseDto>>;

internal sealed class GetAllExercisesHandler : IRequestHandler<GetAllExercises, PagedResultDto<ExerciseDto>>
{
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IMapper _mapper;

    public GetAllExercisesHandler(IExerciseRepository exerciseRepository, IMapper mapper)
    {
        _exerciseRepository = exerciseRepository;
        _mapper = mapper;
    }

    public async Task<PagedResultDto<ExerciseDto>> Handle(GetAllExercises request, CancellationToken cancellationToken)
    {
        var (items, totalItemCount) = await _exerciseRepository.GetAllAsync(request.PageSize, request.PageNumber);
        var result = new PagedResultDto<ExerciseDto>(_mapper.Map<List<ExerciseDto>>(items), totalItemCount, request.PageSize, request.PageNumber);

        return result;
    }
}
