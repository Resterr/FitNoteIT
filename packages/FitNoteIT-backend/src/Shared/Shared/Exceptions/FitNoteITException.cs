using System.Net;

namespace FitNoteIT.Shared.Exceptions;

public abstract class FitNoteITException : Exception
{
	public abstract HttpStatusCode StatusCode { get; }
	protected FitNoteITException(string message) : base(message)
	{

	}
}