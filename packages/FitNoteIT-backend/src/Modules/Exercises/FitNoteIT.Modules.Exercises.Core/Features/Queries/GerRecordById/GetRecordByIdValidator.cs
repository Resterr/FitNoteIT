using FluentValidation;

namespace FitNoteIT.Modules.Exercises.Core.Features.Queries.GerRecordById;
public class GetRecordByIdValidator : AbstractValidator<GetRecordById>
{
	public GetRecordByIdValidator()
	{
		RuleFor(x => x.Id).NotEmpty();
	}
}
