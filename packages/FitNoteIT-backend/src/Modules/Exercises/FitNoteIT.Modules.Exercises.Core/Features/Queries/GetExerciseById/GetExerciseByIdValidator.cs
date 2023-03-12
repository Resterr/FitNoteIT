using FluentValidation;

namespace FitNoteIT.Modules.Exercises.Core.Features.Queries.GetExerciseById;
public class GetExerciseByIdValidator : AbstractValidator<GetExerciseById>
{
	public GetExerciseByIdValidator()
	{
        RuleFor(x => x.Id).NotEmpty();
    }
}
