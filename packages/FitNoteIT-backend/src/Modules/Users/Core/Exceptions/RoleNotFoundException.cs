using FitNoteIT.Shared.Exceptions;
using System.Net;

namespace FitNoteIT.Modules.Users.Core.Exceptions;

public class RoleNotFoundException : FitNoteITException
{
	public RoleNotFoundException(Guid id) : base($"Role with id: {id} not found")
	{
	}

	public RoleNotFoundException(string name) : base($"Role with name: {name} not found")
	{
	}

	public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
}
