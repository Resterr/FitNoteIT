using System.Net;

namespace FitNoteIT.Shared.Exceptions;

public class AccessForbiddenException : FitNoteITException
{
	public AccessForbiddenException() : base("Access forbidden")
	{
	}

	public override HttpStatusCode StatusCode => HttpStatusCode.Forbidden;
}