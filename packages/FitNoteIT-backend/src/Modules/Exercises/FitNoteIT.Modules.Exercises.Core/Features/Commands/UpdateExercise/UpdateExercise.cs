using FitNoteIT.Modules.Exercises.Core.Abstractions.Repositories;
using FitNoteIT.Shared.Exceptions;
using MediatR;

namespace FitNoteIT.Modules.Exercises.Core.Features.Commands.UpdateExercise;
public record UpdateExercise(Guid Id, string Name, string Description) : IRequest<Unit>;

internal sealed class UpdateExerciseHandler : IRequestHandler<UpdateExercise, Unit>
{
    private readonly IExerciseRepository _exerciseRepository;

    public UpdateExerciseHandler(IExerciseRepository exerciseRepository)
    {
        _exerciseRepository = exerciseRepository;
    }

    public async Task<Unit> Handle(UpdateExercise request, CancellationToken cancellationToken)
    {
        var exercise = await _exerciseRepository.GetByIdAsync(request.Id);

        if (exercise is null) throw new NotFoundException("Exercise not found");

        exercise.Update(request.Name, request.Description);
        
        await _exerciseRepository.UpdateAsync(exercise);

        return Unit.Value;
    }
}
