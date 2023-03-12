using FitNoteIT.Modules.Exercises.Core.Abstractions.Repositories;
using FitNoteIT.Shared.Exceptions;
using MediatR;

namespace FitNoteIT.Modules.Exercises.Core.Features.Commands.DeleteExercise;
public record DeleteExercise(Guid Id) : IRequest<Unit>;

internal sealed class DeleteExerciseHandler : IRequestHandler<DeleteExercise, Unit>
{
    private readonly IExerciseRepository _exerciseRepository;

    public DeleteExerciseHandler(IExerciseRepository exerciseRepository)
    {
        _exerciseRepository = exerciseRepository;
    }

    public async Task<Unit> Handle(DeleteExercise request, CancellationToken cancellationToken)
    {
        var exercise = await _exerciseRepository.GetByIdAsync(request.Id);

        if (exercise is null) throw new NotFoundException("Exercise not found");

        await _exerciseRepository.DeleteAsync(exercise);

        return Unit.Value;
    }
}