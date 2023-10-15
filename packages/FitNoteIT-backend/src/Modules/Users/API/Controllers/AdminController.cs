using FitNoteIT.Modules.Users.Core.Features.AdminFeature.Commands;
using FitNoteIT.Modules.Users.Core.Features.AdminFeature.Queries;
using FitNoteIT.Shared.Dispatchers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FitNoteIT.Modules.Users.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
	private readonly IDispatcher _dispatcher;

	public AdminController(IDispatcher dispatcher)
	{
		_dispatcher = dispatcher;
	}

	[HttpGet("roles/{id}")]
	[Authorize(Roles = "Admin")]
	[SwaggerOperation("Get roles for user")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> GetRolesForUser([FromRoute] Guid id)
	{
		var request = new GetRolesForUser(id);
		var result = await _dispatcher.QueryAsync(request);
		return Ok(result);
	}

	[HttpPatch("grant/{id}")]
	[Authorize(Roles = "Superadmin")]
	[SwaggerOperation("Grant user admin role")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> GrantAdmin([FromRoute] Guid id)
	{
		var request = new GrantAdminRole(id);
		await _dispatcher.SendAsync(request);
		return Ok();
	}

	[HttpPatch("remove/{id}")]
	[Authorize(Roles = "Superadmin")]
	[SwaggerOperation("Remove user from admin role")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> RemoveAdmin([FromRoute] Guid id)
	{
		var request = new RemoveAdminRole(id);
		await _dispatcher.SendAsync(request);
		return Ok();
	}
}
