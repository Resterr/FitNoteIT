using System.Net;
using FitNoteIT.Shared.Exceptions;

namespace FitNoteIT.Modules.Users.Core.Exceptions;

public class UserNotFoundException : FitNoteITException
{
	public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
	
	public UserNotFoundException() : base($"Current user not found") { }

	public UserNotFoundException(Guid id) : base($"User with id: {id} not found") { }

	public UserNotFoundException(string value, string type) : base($"User with {type}: {value} not found") { }
}