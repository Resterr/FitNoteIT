using FitNoteIT.Modules.Users.Core.Features.AdminFeature.Commands;
using FitNoteIT.Modules.Users.Core.Features.AdminFeature.Queries;
using FitNoteIT.Modules.Users.Shared.DTO;
using FitNoteIT.Shared.Dispatchers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using Swashbuckle.AspNetCore.Annotations;

namespace FitNoteIT.Modules.Users.API.Requests;
internal static class AdminRequests
{
	public static WebApplication RegisterAdminRequests(this WebApplication app)
	{
		app.MapGroup("api/admin/")
			.MapAdminEndpoints()
			.WithTags("Admin")
			.AddFluentValidationAutoValidation();

		return app;
	}

	private static RouteGroupBuilder MapAdminEndpoints(this RouteGroupBuilder group)
	{
		group.MapGet("users", async (IDispatcher dispatcher, [AsParameters] GetAllUsers request) =>
			{
				var result = await dispatcher.QueryAsync(request);
				return Results.Ok(result);
			}).RequireAuthorization("admin")
			.Produces<UserDto>()
			.Produces(StatusCodes.Status401Unauthorized)
			.Produces(StatusCodes.Status403Forbidden)
			.WithMetadata(new SwaggerOperationAttribute("Get all users"));
		
		group.MapGet("users/{id:guid}", async (IDispatcher dispatcher, Guid id) =>
			{
				var request = new GetUserById(id);
				var result = await dispatcher.QueryAsync(request);
				return Results.Ok(result);
			}).RequireAuthorization("admin")
			.Produces<UserDto>()
			.Produces(StatusCodes.Status401Unauthorized)
			.Produces(StatusCodes.Status403Forbidden)
			.Produces(StatusCodes.Status404NotFound)
			.WithMetadata(new SwaggerOperationAttribute("Get user by id"));
		
		group.MapGet("role/user/{id:guid}", async (IDispatcher dispatcher, Guid id) =>
		{
			var request = new GetRolesForUser(id);
			var result = await dispatcher.QueryAsync(request);
			return Results.Ok(result);
		}).RequireAuthorization("admin")
			.Produces<List<RoleDto>>()
			.Produces(StatusCodes.Status401Unauthorized)
			.Produces(StatusCodes.Status403Forbidden)
			.Produces(StatusCodes.Status404NotFound)
			.WithMetadata(new SwaggerOperationAttribute("Get roles for user"));

		group.MapPatch("role/grant", async (IDispatcher dispatcher, GrantRole request) =>
		{
			await dispatcher.SendAsync(request);
			return Results.Ok();
		}).RequireAuthorization("admin")
			.Produces(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status400BadRequest)
			.Produces(StatusCodes.Status401Unauthorized)
			.Produces(StatusCodes.Status403Forbidden)
			.Produces(StatusCodes.Status404NotFound)
			.WithMetadata(new SwaggerOperationAttribute("Grant role"));

		group.MapPatch("role/remove", async (IDispatcher dispatcher, RemoveRole request) =>
		{
			await dispatcher.SendAsync(request);
			return Results.Ok();
		}).RequireAuthorization("admin")
			.Produces(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status400BadRequest)
			.Produces(StatusCodes.Status401Unauthorized)
			.Produces(StatusCodes.Status403Forbidden)
			.Produces(StatusCodes.Status404NotFound)
			.WithMetadata(new SwaggerOperationAttribute("Remove role"));

		group.MapDelete("users/{id:guid}", async (IDispatcher dispatcher, Guid id) =>
			{
				var request = new RemoveUser(id);
				await dispatcher.SendAsync(request);
				return Results.NoContent();
			}).RequireAuthorization("admin")
			.Produces(StatusCodes.Status204NoContent)
			.Produces(StatusCodes.Status401Unauthorized)
			.Produces(StatusCodes.Status403Forbidden)
			.Produces(StatusCodes.Status404NotFound)
			.WithMetadata(new SwaggerOperationAttribute("Remove user"));
		
		return group;
	}
}
