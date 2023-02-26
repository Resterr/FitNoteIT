using FluentValidation;

namespace FitNoteIT.Modules.Users.Core.Features.Queries.GetAllUsers;
public class GetAllUsersValidator : AbstractValidator<GetAllUsers>
{
	public GetAllUsersValidator()
	{
        RuleFor(x => x.PageSize).GreaterThan(0);
        RuleFor(x => x.PageNumber).GreaterThan(0);
    }
}
