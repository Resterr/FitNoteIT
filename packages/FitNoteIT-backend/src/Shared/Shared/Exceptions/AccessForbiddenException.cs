using System.Net;

namespace FitNoteIT.Shared.Exceptions;

public class AccessForbiddenException : FitNoteITException
{
	public override HttpStatusCode StatusCode => HttpStatusCode.Forbidden;

	public AccessForbiddenException() : base("Access forbidden") { }
}