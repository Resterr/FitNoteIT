using FluentValidation;

namespace FitNoteIT.Modules.Exercises.Core.Features.Commands.CreateExercise;
public class CreateExerciseValidator : AbstractValidator<CreateExercise>
{
	public CreateExerciseValidator()
	{
		RuleFor(x => x.Name).NotEmpty();
    }
}
