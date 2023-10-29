using System.Net;
using FitNoteIT.Shared.Exceptions;

namespace FitNoteIT.Modules.Users.Core.Exceptions;

public class RoleNotFoundException : FitNoteITException
{
	public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;

	public RoleNotFoundException(Guid id) : base($"Role with id: {id} not found") { }

	public RoleNotFoundException(string name) : base($"Role with name: {name} not found") { }
}