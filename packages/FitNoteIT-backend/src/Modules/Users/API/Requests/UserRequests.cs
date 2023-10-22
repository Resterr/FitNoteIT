using FitNoteIT.Modules.Users.Core.Features.UserFeature.Commands;
using FitNoteIT.Modules.Users.Core.Features.UserFeature.Queries;
using FitNoteIT.Modules.Users.Shared.DTO;
using FitNoteIT.Shared.Dispatchers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using Swashbuckle.AspNetCore.Annotations;

namespace FitNoteIT.Modules.Users.API.Requests;
internal static class UsersRequests
{
	public static WebApplication RegisterUsersRequests(this WebApplication app)
	{
		app.MapGroup("api/users/")
			.MapUsersEndpoints()
			.WithTags("Users")
			.AddFluentValidationAutoValidation();

		return app;
	}

	private static RouteGroupBuilder MapUsersEndpoints(this RouteGroupBuilder group)
	{
		group.MapGet("", async (IDispatcher dispatcher, [AsParameters] GetAllUsers request) =>
			{
				var result = await dispatcher.QueryAsync(request);
				return Results.Ok(result);
			}).RequireAuthorization("admin")
			.Produces<UserDto>(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status401Unauthorized)
			.Produces(StatusCodes.Status403Forbidden)
			.Produces(StatusCodes.Status404NotFound)
			.WithMetadata(new SwaggerOperationAttribute("Get all users"));
		
		group.MapGet("{id}", async (IDispatcher dispatcher, Guid id) =>
		{
			var request = new GetUserById(id);
			var result = await dispatcher.QueryAsync(request);
			return Results.Ok(result);
		}).RequireAuthorization("admin")
			.Produces<UserDto>(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status401Unauthorized)
			.Produces(StatusCodes.Status403Forbidden)
			.Produces(StatusCodes.Status404NotFound)
			.WithMetadata(new SwaggerOperationAttribute("Get user by id"));

		group.MapGet("current", async (IDispatcher dispatcher) =>
		{
			var request = new SelfGetUser();
			var result = await dispatcher.QueryAsync(request);
			return Results.Ok(result);
		}).RequireAuthorization("user")
			.Produces<UserDto>(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status401Unauthorized)
			.WithMetadata(new SwaggerOperationAttribute("Get current user"));

		group.MapPost("register", async (IDispatcher dispatcher, RegisterUser request) =>
		{
			await dispatcher.SendAsync(request);
			return Results.Ok();
		}).AllowAnonymous()
			.Produces(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status400BadRequest)
			.WithMetadata(new SwaggerOperationAttribute("Sign up user"));

		group.MapPost("login", async (IDispatcher dispatcher, LoginUser request) =>
		{
			var result = await dispatcher.QueryAsync(request);
			return Results.Ok(result);
		}).AllowAnonymous()
			.Produces<TokensDto>(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status400BadRequest)
			.Produces(StatusCodes.Status404NotFound)
			.WithMetadata(new SwaggerOperationAttribute("Sign in user"));

		group.MapPost("token/refresh", async (IDispatcher dispatcher, TokenRefresh request) =>
		{
			var token = await dispatcher.QueryAsync(request);
			return Results.Ok(token);
		}).RequireAuthorization("user")
			.Produces<TokensDto>(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status400BadRequest)
			.Produces(StatusCodes.Status401Unauthorized)
			.Produces(StatusCodes.Status404NotFound)
			.WithMetadata(new SwaggerOperationAttribute("Refresh token"));

		group.MapPatch("token/remove", async (IDispatcher dispatcher, TokenRemove request) =>
		{
			await dispatcher.SendAsync(request);
			return Results.NoContent();
		}).RequireAuthorization("user")
			.Produces(StatusCodes.Status204NoContent)
			.Produces(StatusCodes.Status400BadRequest)
			.Produces(StatusCodes.Status401Unauthorized)
			.Produces(StatusCodes.Status404NotFound)
			.WithMetadata(new SwaggerOperationAttribute("Remove refresh token"));

		return group;
	}
}

