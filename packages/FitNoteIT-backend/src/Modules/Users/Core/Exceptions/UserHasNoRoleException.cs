using System.Net;
using FitNoteIT.Shared.Exceptions;

namespace FitNoteIT.Modules.Users.Core.Exceptions;

public class UserHasNoRoleException : FitNoteITException
{
	public UserHasNoRoleException(Guid id, string roleName) : base($"User with id: {id} has no role {roleName}")
	{
	}
		
	public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}