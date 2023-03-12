using AutoMapper;
using FitNoteIT.Modules.Exercises.Core.Abstractions.Repositories;
using FitNoteIT.Modules.Exercises.Shared.DTO;
using FitNoteIT.Shared.Exceptions;
using MediatR;

namespace FitNoteIT.Modules.Exercises.Core.Features.Queries.GetExerciseById;
public record GetExerciseById(Guid Id) : IRequest<ExerciseDto>;

internal sealed class GetExerciseByIdHandler : IRequestHandler<GetExerciseById, ExerciseDto>
{
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IMapper _mapper;

    public GetExerciseByIdHandler(IExerciseRepository exerciseRepository, IMapper mapper)
    {
        _exerciseRepository = exerciseRepository;
        _mapper = mapper;
    }

    public async Task<ExerciseDto> Handle(GetExerciseById request, CancellationToken cancellationToken)
    {
        var exercise = await _exerciseRepository.GetByIdAsync(request.Id);

        if (exercise is null) throw new NotFoundException("Exercise not found");

        var result = _mapper.Map<ExerciseDto>(exercise);

        return result;
    }
}
