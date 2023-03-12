using FitNoteIT.Modules.Exercises.Core.Abstractions.Factories;
using FitNoteIT.Modules.Exercises.Core.Abstractions.Repositories;
using FitNoteIT.Shared.Exceptions;
using MediatR;

namespace FitNoteIT.Modules.Exercises.Core.Features.Commands.CreateExercise;
public record CreateExercise(string Name, string Description) : IRequest<Guid>;

internal sealed class CreateExerciseHandler : IRequestHandler<CreateExercise, Guid>
{
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IExerciseFactory _exerciseFactory;

    public CreateExerciseHandler(IExerciseRepository exerciseRepository, IExerciseFactory exerciseFactory)
    {
        _exerciseRepository = exerciseRepository;
        _exerciseFactory = exerciseFactory;
    }

    public async Task<Guid> Handle(CreateExercise request, CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();
        var name = request.Name;

        if (await _exerciseRepository.GetByNameAsync(name) is not null) throw new BadRequestException("Exercise already exists");

        var exercise = _exerciseFactory.Create(id, name, request.Description);

        await _exerciseRepository.AddAsync(exercise);

        return id;
    }
}
