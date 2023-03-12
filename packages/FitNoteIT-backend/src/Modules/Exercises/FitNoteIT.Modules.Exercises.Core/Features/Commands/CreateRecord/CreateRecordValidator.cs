using FluentValidation;

namespace FitNoteIT.Modules.Exercises.Core.Features.Commands.CreateRecord;
public class CreateRecordValidator : AbstractValidator<CreateRecord>
{
	public CreateRecordValidator()
	{
		RuleFor(x => x.ExerciseId).NotEmpty();
        RuleFor(x => x.Result).NotEmpty().GreaterThanOrEqualTo(0);
		RuleFor(x => x.RecordDate).NotEmpty();
    }
}
