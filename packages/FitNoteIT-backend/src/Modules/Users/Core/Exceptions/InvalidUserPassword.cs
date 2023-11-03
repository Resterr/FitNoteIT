using System.Net;
using FitNoteIT.Shared.Exceptions;

namespace FitNoteIT.Modules.Users.Core.Exceptions;

internal class InvalidUserPassword : FitNoteITException
{
	public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

	public InvalidUserPassword() : base("Invalid password") { }
}