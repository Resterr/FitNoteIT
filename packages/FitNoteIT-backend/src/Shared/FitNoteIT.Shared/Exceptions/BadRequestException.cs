using System.Net;

namespace FitNoteIT.Shared.Exceptions;
public class BadRequestException : CustomException
{
	public BadRequestException(string messege) : base("Bad Request", messege) { }

	public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}
