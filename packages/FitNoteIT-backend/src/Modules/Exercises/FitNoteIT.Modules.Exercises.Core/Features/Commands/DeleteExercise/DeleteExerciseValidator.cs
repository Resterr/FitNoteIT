using FluentValidation;

namespace FitNoteIT.Modules.Exercises.Core.Features.Commands.DeleteExercise;
public class DeleteExerciseValidator : AbstractValidator<DeleteExercise>
{
	public DeleteExerciseValidator()
	{
		RuleFor(x => x.ExerciseName).NotEmpty();
	}
}
