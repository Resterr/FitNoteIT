using FitNoteIT.Modules.Exercises.Core.Features.Commands.CreateExercise;
using FitNoteIT.Modules.Exercises.Core.Features.Commands.DeleteExercise;
using FitNoteIT.Modules.Exercises.Core.Features.Commands.UpdateExercise;
using FitNoteIT.Modules.Exercises.Core.Features.Queries.GetAllExercises;
using FitNoteIT.Modules.Exercises.Core.Features.Queries.GetExerciseById;
using FitNoteIT.Modules.Exercises.Shared.DTO;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitNoteIT.Modules.Exercises.Api.Requests;
internal static class Exercises
{
    public static WebApplication RegisterExercisesRequests(this WebApplication app)
    {
        app.MapGet("/exercises", Exercises.GetAllExercises)
            .RequireAuthorization()
            .Produces<List<ExerciseDto>>()
            .Produces(StatusCodes.Status200OK);

        app.MapGet("/exercises/{id}", Exercises.GetExerciseById)
            .RequireAuthorization()
            .Produces<ExerciseDto>()
            .Produces(StatusCodes.Status200OK);

        app.MapPost("/exercises", Exercises.CreateExercise)
            .RequireAuthorization("is-admin")
            .Accepts<CreateExercise>("application/json")
            .Produces(StatusCodes.Status201Created);

        app.MapPut("/exercises", Exercises.UpdateExercise)
            .RequireAuthorization("is-admin")
            .Accepts<UpdateExercise>("application/json")
            .Produces(StatusCodes.Status200OK);

        app.MapDelete("/exercises/{id}", Exercises.DeleteExercise)
            .RequireAuthorization("is-admin")
            .Produces(StatusCodes.Status204NoContent);

        return app;
    }

    private static async Task<IResult> GetAllExercises(IMediator mediator, [AsParameters] GetAllExercises request)
    {
        var exercise = await mediator.Send(request);
        return Results.Ok(exercise);
    }

    private static async Task<IResult> GetExerciseById(IMediator mediator, Guid id)
    {
        var request = new GetExerciseById(id);
        var exercise = await mediator.Send(request);
        return Results.Ok(exercise);
    }

    private static async Task<IResult> CreateExercise(IMediator mediator, [FromBody] CreateExercise request)
    {
        var id = await mediator.Send(request);
        return Results.Created($"/exercises/{id}", "");
    }

    private static async Task<IResult> UpdateExercise(IMediator mediator, [FromBody] UpdateExercise request)
    {
        await mediator.Send(request);
        return Results.Ok();
    }

    private static async Task<IResult> DeleteExercise(IMediator mediator, string exerciseName)
    {
        var request = new DeleteExercise(exerciseName);
        await mediator.Send(request);
        return Results.NoContent();
    }
}
