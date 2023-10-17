using FitNoteIT.Shared.Exceptions;
using System.Net;

namespace FitNoteIT.Modules.Users.Core.Exceptions;

public class InvalidTokenException : FitNoteITException
{
	public InvalidTokenException() : base($"Invalid token")
	{
	}

	public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}
