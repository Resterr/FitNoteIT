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

internal static class TrainingsRequests
{
	public static WebApplication RegisterTrainingsRequests(this WebApplication app)
	{
		app.MapGroup("api/trainings/")
			.MapTrainingsEndpoints()
			.WithTags("Trainings")
			.AddFluentValidationAutoValidation();

		return app;
	}

	private static RouteGroupBuilder MapTrainingsEndpoints(this RouteGroupBuilder group)
	{
		group.MapGet("history", async (IDispatcher dispatcher, [AsParameters] GetTrainingHistory request) =>
			{
				var result = await dispatcher.QueryAsync(request);

				return Results.Ok(result);
			})
			.RequireAuthorization("user")
			.Produces<List<TrainingDto>>()
			.Produces(StatusCodes.Status401Unauthorized)
			.Produces(StatusCodes.Status404NotFound)
			.WithMetadata(new SwaggerOperationAttribute("Training history for current user"));

		group.MapGet("{id:guid}", async (IDispatcher dispatcher, [AsParameters] GetTrainingById request) =>
			{
				var result = await dispatcher.QueryAsync(request);

				return Results.Ok(result);
			})
			.RequireAuthorization("user")
			.Produces<TrainingDto>()
			.Produces(StatusCodes.Status401Unauthorized)
			.Produces(StatusCodes.Status404NotFound)
			.WithMetadata(new SwaggerOperationAttribute("Training by id for current user"));
		
		group.MapPost("", async (IDispatcher dispatcher, [FromBody] CreateTraining request) =>
			{
				await dispatcher.SendAsync(request);
				return Results.Ok();
			})
			.RequireAuthorization("user")
			.Produces(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status400BadRequest)
			.Produces(StatusCodes.Status401Unauthorized)
			.Produces(StatusCodes.Status404NotFound)
			.WithMetadata(new SwaggerOperationAttribute("Create training for current user"));

		group.MapDelete("{id:guid}", async (IDispatcher dispatcher, Guid id) =>
			{
				var request = new RemoveTraining(id);
				await dispatcher.SendAsync(request);
				return Results.NoContent();
			})
			.RequireAuthorization("user")
			.Produces(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status400BadRequest)
			.Produces(StatusCodes.Status401Unauthorized)
			.Produces(StatusCodes.Status404NotFound)
			.WithMetadata(new SwaggerOperationAttribute("Delete training for current user"));

		return group;
	}
}