using System.Net;

namespace FitNoteIT.Shared.Exceptions;
public abstract class CustomException : Exception
{
	public string Title { get; set; }
	public abstract HttpStatusCode StatusCode { get; }
	protected CustomException(string title, string message) : base(message) 
	{
		Title = title;
	}
}
