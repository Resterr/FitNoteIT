using FluentValidation;

namespace FitNoteIT.Modules.Exercises.Core.Features.Commands.UpdateExercise;
public class UpdateExerciseValidator : AbstractValidator<UpdateExercise>
{
	public UpdateExerciseValidator()
	{
		RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
    }
}
