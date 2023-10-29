using System.Net;
using FitNoteIT.Shared.Exceptions;

namespace FitNoteIT.Modules.Users.Core.Exceptions;

public class UserHasRoleException : FitNoteITException
{
	public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

	public UserHasRoleException(Guid id, string roleName) : base($"User with id: {id} has role {roleName}") { }
}