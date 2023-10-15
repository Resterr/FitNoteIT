using FitNoteIT.Shared.Exceptions;
using System.Net;

namespace FitNoteIT.Modules.Users.Core.Exceptions;

public class UserAlreadyVerifiedException : FitNoteITException
{
	public UserAlreadyVerifiedException(Guid id) : base($"User with id: {id} already verified")
	{
	}

	public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
}