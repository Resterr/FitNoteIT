using FitNoteIT.Modules.Exercises.Core.Features.RecordFeature.Commands;
using FitNoteIT.Modules.Exercises.Core.Features.RecordFeature.Queries;
using FitNoteIT.Modules.Exercises.Shared.DTO;
using FitNoteIT.Shared.Dispatchers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using Swashbuckle.AspNetCore.Annotations;

namespace FitNoteIT.Modules.Exercises.API.Requests;

internal static class RecordRequests
{
	public static WebApplication RegisterRecordsRequests(this WebApplication app)
	{
		app.MapGroup("api/records/")
			.MapRecordsEndpoints()
			.WithTags("Records")
			.AddFluentValidationAutoValidation();

		return app;
	}

	private static RouteGroupBuilder MapRecordsEndpoints(this RouteGroupBuilder group)
	{
		group.MapGet("", async (IDispatcher dispatcher, [AsParameters] GetAllRecordsForCurrentUser request) =>
			{
				var result = await dispatcher.QueryAsync(request);
				
				return Results.Ok(result);
			})
			.RequireAuthorization("user")
			.Produces<List<RecordDto>>()
			.Produces(StatusCodes.Status400BadRequest)
			.Produces(StatusCodes.Status401Unauthorized)
			.Produces(StatusCodes.Status404NotFound)
			.WithMetadata(new SwaggerOperationAttribute("Get all records for current user"));

		group.MapPut("", async (IDispatcher dispatcher, CreateOrUpdateRecordForCurrentUser request) =>
			{
				await dispatcher.SendAsync(request);
				return Results.Ok();
			})
			.RequireAuthorization("user")
			.Produces(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status400BadRequest)
			.Produces(StatusCodes.Status401Unauthorized)
			.Produces(StatusCodes.Status404NotFound)
			.WithMetadata(new SwaggerOperationAttribute("Create or update record for current user"));

		group.MapDelete("{exerciseId:guid}", async (IDispatcher dispatcher, Guid exerciseId) =>
			{
				var request = new RemoveRecordForCurrentUser(exerciseId);
				await dispatcher.SendAsync(request);
				return Results.NoContent();
			})
			.RequireAuthorization("user")
			.Produces(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status400BadRequest)
			.Produces(StatusCodes.Status401Unauthorized)
			.Produces(StatusCodes.Status404NotFound)
			.WithMetadata(new SwaggerOperationAttribute("Delete record for current user"));

		return group;
	}
}