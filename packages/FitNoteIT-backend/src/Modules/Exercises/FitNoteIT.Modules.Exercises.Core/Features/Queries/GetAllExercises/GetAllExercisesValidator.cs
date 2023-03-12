using FluentValidation;

namespace FitNoteIT.Modules.Exercises.Core.Features.Queries.GetAllExercises;
public class GetAllExercisesValidator : AbstractValidator<GetAllExercises>
{
	public GetAllExercisesValidator()
	{
        RuleFor(x => x.PageSize).GreaterThan(0);
        RuleFor(x => x.PageNumber).GreaterThan(0);
    }
}
