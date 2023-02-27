using FluentValidation;

namespace FitNoteIT.Modules.Users.Core.Features.Queries.GetUserById;
public class GetUserByIdValidator : AbstractValidator<GetUserById>
{
	public GetUserByIdValidator()
	{
		RuleFor(x => x.Id).NotEmpty();
	}
}
