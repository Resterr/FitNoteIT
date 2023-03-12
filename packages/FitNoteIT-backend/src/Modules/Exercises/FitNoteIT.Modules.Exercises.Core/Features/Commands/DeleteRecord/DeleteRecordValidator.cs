using FluentValidation;

namespace FitNoteIT.Modules.Exercises.Core.Features.Commands.DeleteRecord;
public class DeleteRecordValidator : AbstractValidator<DeleteRecord>
{
	public DeleteRecordValidator()
	{
		RuleFor(x => x.Id).NotEmpty();
	}
}
