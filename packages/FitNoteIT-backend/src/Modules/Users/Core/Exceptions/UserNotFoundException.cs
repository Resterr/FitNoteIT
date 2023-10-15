using FitNoteIT.Shared.Exceptions;
using System.Net;

namespace FitNoteIT.Modules.Users.Core.Exceptions;

public class UserNotFoundException : FitNoteITException
{
	public UserNotFoundException(Guid id) : base($"User with id: {id} not found")
	{
	}

	public UserNotFoundException(string value, string type) : base($"User with {type}: {value} not found")
	{
	}

	public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
}
