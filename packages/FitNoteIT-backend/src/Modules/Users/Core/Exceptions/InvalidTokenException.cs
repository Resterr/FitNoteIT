using System.Net;
using FitNoteIT.Shared.Exceptions;

namespace FitNoteIT.Modules.Users.Core.Exceptions;

public class InvalidTokenException : FitNoteITException
{
	public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

	public InvalidTokenException() : base("Invalid token") { }
}