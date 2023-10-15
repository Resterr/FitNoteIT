using FitNoteIT.Shared.Exceptions;
using System.Net;

namespace FitNoteIT.Modules.Users.Core.Exceptions;

internal class InvalidUserCredentials : FitNoteITException
{
	public InvalidUserCredentials() : base($"Invalid user credentials")
	{
	}

	public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}
