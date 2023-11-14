using System.Net;

namespace FitNoteIT.Shared.Exceptions;

public class InvalidDateFormat : FitNoteITException
{
	public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

	public InvalidDateFormat(string format) : base($"Invalid date format - should be {format}") { }
}