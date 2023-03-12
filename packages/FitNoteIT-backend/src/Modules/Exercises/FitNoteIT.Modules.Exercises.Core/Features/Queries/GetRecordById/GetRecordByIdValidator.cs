using FluentValidation;

namespace FitNoteIT.Modules.Exercises.Core.Features.Queries.GetRecordById;
public class GetRecordByIdValidator : AbstractValidator<GetRecordById>
{
	public GetRecordByIdValidator()
	{
		RuleFor(x => x.Id).NotEmpty();
	}
}
