using FitNoteIT.Shared.Exceptions;
using System.Net;

namespace FitNoteIT.Modules.Users.Core.Exceptions;
internal class InvalidUserPassword : FitNoteITException
{
	public InvalidUserPassword() : base($"Invalid password")
	{
	}

	public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}
