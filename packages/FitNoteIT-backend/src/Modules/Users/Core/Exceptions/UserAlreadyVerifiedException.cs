using System.Net;
using FitNoteIT.Shared.Exceptions;

namespace FitNoteIT.Modules.Users.Core.Exceptions;

public class UserAlreadyVerifiedException : FitNoteITException
{
	public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;

	public UserAlreadyVerifiedException(Guid id) : base($"User with id: {id} already verified") { }
}