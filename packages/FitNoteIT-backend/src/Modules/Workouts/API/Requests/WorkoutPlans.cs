using FitNoteIT.Modules.Workouts.Core.Features.Commands;
using FitNoteIT.Modules.Workouts.Core.Features.Queries;
using FitNoteIT.Modules.Workouts.Shared.DTO;
using FitNoteIT.Shared.Dispatchers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using Swashbuckle.AspNetCore.Annotations;

namespace FitNoteIT.Modules.Workouts.API.Requests;

internal static class WorkoutPlanRequests
{
	public static WebApplication RegisterWorkoutPlansRequests(this WebApplication app)
	{
		app.MapGroup("api/workouts/plans/")
			.MapWorkoutPlansEndpoints()
			.WithTags("Workout plans")
			.AddFluentValidationAutoValidation();

		return app;
	}

	private static RouteGroupBuilder MapWorkoutPlansEndpoints(this RouteGroupBuilder group)
	{
		group.MapGet("", async (IDispatcher dispatcher, [AsParameters] GetAllWorkoutPlansForUser request) =>
			{
				var result = await dispatcher.QueryAsync(request);
				
				return Results.Ok(result);
			})
			.RequireAuthorization("user")
			.Produces<List<WorkoutPlanDto>>()
			.Produces(StatusCodes.Status401Unauthorized)
			.Produces(StatusCodes.Status404NotFound)
			.WithMetadata(new SwaggerOperationAttribute("Get all workout plans for current user"));

		group.MapPost("", async (IDispatcher dispatcher, [FromBody] CreateWorkoutPlan request) =>
			{
				await dispatcher.SendAsync(request);
				return Results.Ok();
			})
			.RequireAuthorization("user")
			.Produces(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status400BadRequest)
			.Produces(StatusCodes.Status401Unauthorized)
			.Produces(StatusCodes.Status404NotFound)
			.WithMetadata(new SwaggerOperationAttribute("Create workout plan for current user"));

		group.MapDelete("{id:guid}", async (IDispatcher dispatcher, Guid id) =>
			{
				var request = new RemoveWorkoutPlan(id);
				await dispatcher.SendAsync(request);
				return Results.NoContent();
			})
			.RequireAuthorization("user")
			.Produces(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status400BadRequest)
			.Produces(StatusCodes.Status401Unauthorized)
			.Produces(StatusCodes.Status404NotFound)
			.WithMetadata(new SwaggerOperationAttribute("Delete workout plan for current user"));

		return group;
	}
}
