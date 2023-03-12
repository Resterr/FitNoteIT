using FluentValidation;

namespace FitNoteIT.Modules.Exercises.Core.Features.Queries.GetAllRecordsForUser;
public class GetAllRecordsForUserValidator : AbstractValidator<GetAllRecordsForUser>
{
	public GetAllRecordsForUserValidator()
	{
        RuleFor(x => x.PageSize).GreaterThan(0);
        RuleFor(x => x.PageNumber).GreaterThan(0);
    }
}
