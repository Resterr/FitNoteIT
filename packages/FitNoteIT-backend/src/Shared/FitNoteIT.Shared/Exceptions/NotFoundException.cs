using System.Net;

namespace FitNoteIT.Shared.Exceptions;
public class NotFoundException : CustomException
{
	public NotFoundException(string messege) : base("Not Found", messege) { }

	public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
}
