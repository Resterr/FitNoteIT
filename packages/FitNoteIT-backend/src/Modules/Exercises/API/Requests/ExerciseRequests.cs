using FitNoteIT.Modules.Exercises.Core.Features.ExerciseFeature.Queries;
using FitNoteIT.Modules.Exercises.Shared.DTO;
using FitNoteIT.Shared.Dispatchers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using Swashbuckle.AspNetCore.Annotations;

namespace FitNoteIT.Modules.Exercises.API.Requests;

internal static class ExerciseRequests
{
	public static WebApplication RegisterExercisesRequests(this WebApplication app)
	{
		app.MapGroup("api/exercises/")
			.MapExercisesEndpoints()
			.WithTags("Exercises")
			.AddFluentValidationAutoValidation();

		return app;
	}

	private static RouteGroupBuilder MapExercisesEndpoints(this RouteGroupBuilder group)
	{
		group.MapGet("", async (IDispatcher dispatcher, [AsParameters] GetAllExercises request) =>
			{
				var result = await dispatcher.QueryAsync(request);
				return Results.Ok(result);
			})
			.RequireAuthorization("user")
			.Produces<List<ExerciseDto>>()
			.Produces(StatusCodes.Status400BadRequest)
			.Produces(StatusCodes.Status401Unauthorized)
			.Produces(StatusCodes.Status404NotFound)
			.WithMetadata(new SwaggerOperationAttribute("Get all exercises"));

		return group;
	}
}