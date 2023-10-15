using FitNoteIT.Modules.Users.Core.Features.UserFeature.Commands;
using FitNoteIT.Modules.Users.Core.Features.UserFeature.Queries;
using FitNoteIT.Shared.Dispatchers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FitNoteIT.Modules.Users.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
	private readonly IDispatcher _dispatcher;

	public UsersController(IDispatcher dispatcher)
    {
		_dispatcher = dispatcher;
	}

    [HttpGet("{id}")]
	[Authorize(Roles = "Admin")]
	[SwaggerOperation("Get user by id")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> GetUserById(Guid id)
	{
		var request = new GetUserById(id);
		var result = await _dispatcher.QueryAsync(request);
		return Ok(result);
	}

	[HttpGet("current")]
	[Authorize(Roles = "User")]
	[SwaggerOperation("Get current user")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> SelfGetUser()
	{
		var request = new SelfGetUser();
		var result = await _dispatcher.QueryAsync(request);
		return Ok(result);
	}

	[HttpPost("register")]
	[AllowAnonymous]
	[SwaggerOperation("Sign up user")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> RegisterUser([FromBody] RegisterUser request)
	{
		await _dispatcher.SendAsync(request);
		return Ok();
	}

	[HttpPost("login")]
	[AllowAnonymous]
	[SwaggerOperation("Sign in user")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> LoginUser([FromBody] LoginUser request)
	{
		var result = await _dispatcher.QueryAsync(request);
		return Ok(result);
	}

	[HttpPost("token/refresh")]
	[Authorize(Roles = "User")]
	[SwaggerOperation("Refresh token")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> TokenRefresh([FromBody] TokenRefresh request)
	{
		var token = await _dispatcher.QueryAsync(request);
		return Ok(token);
	}

	[HttpPatch("token/remove")]
	[Authorize(Roles = "User")]
	[SwaggerOperation("Remove refresh token")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> TokenRemove([FromBody] TokenRemove request)
	{
		await _dispatcher.SendAsync(request);
		return Ok();
	}
}