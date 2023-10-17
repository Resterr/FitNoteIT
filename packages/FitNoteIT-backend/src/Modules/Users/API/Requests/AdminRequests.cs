using FitNoteIT.Modules.Users.Core.Features.AdminFeature.Commands;
using FitNoteIT.Modules.Users.Core.Features.AdminFeature.Queries;
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
		group.MapGet("roles/{id}", async (IDispatcher dispatcher, Guid id) =>
		{
			var request = new GetRolesForUser(id);
			var result = await dispatcher.QueryAsync(request);
			return Results.Ok(result);
		}).RequireAuthorization("admin")
			.Produces<List<string>>(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status401Unauthorized)
			.Produces(StatusCodes.Status403Forbidden)
			.Produces(StatusCodes.Status404NotFound)
			.WithMetadata(new SwaggerOperationAttribute("Get roles for user"));

		group.MapPatch("grant/{id}", async (IDispatcher dispatcher, Guid id) =>
		{
			var request = new GrantAdminRole(id);
			await dispatcher.SendAsync(request);
			return Results.Ok();
		}).RequireAuthorization("superadmin")
			.Produces(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status401Unauthorized)
			.Produces(StatusCodes.Status403Forbidden)
			.Produces(StatusCodes.Status404NotFound)
			.WithMetadata(new SwaggerOperationAttribute("Grant user admin role"));

		group.MapPatch("remove/{id}", async (IDispatcher dispatcher, Guid id) =>
		{
			var request = new RemoveAdminRole(id);
			await dispatcher.SendAsync(request);
			return Results.Ok();
		}).RequireAuthorization("superadmin")
			.Produces(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status401Unauthorized)
			.Produces(StatusCodes.Status403Forbidden)
			.Produces(StatusCodes.Status404NotFound)
			.WithMetadata(new SwaggerOperationAttribute("Remove user from admin role"));

		return group;
	}
}
