using FluentValidation;

namespace FitNoteIT.Modules.Exercises.Core.Features.Commands.UpdateRecord;
public class UpdateRecordValidator : AbstractValidator<UpdateRecord>
{
    public UpdateRecordValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Result).NotEmpty().GreaterThanOrEqualTo(0);
        RuleFor(x => x.RecordDate).NotEmpty();
    }
}
