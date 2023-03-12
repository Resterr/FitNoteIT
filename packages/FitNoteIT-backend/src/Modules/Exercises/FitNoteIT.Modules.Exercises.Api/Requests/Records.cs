using FitNoteIT.Modules.Exercises.Core.Features.Commands.CreateRecord;
using FitNoteIT.Modules.Exercises.Core.Features.Commands.DeleteRecord;
using FitNoteIT.Modules.Exercises.Core.Features.Commands.UpdateRecord;
using FitNoteIT.Modules.Exercises.Core.Features.Queries.GerRecordById;
using FitNoteIT.Modules.Exercises.Core.Features.Queries.GetAllRecordsForUser;
using FitNoteIT.Modules.Exercises.Shared.DTO;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitNoteIT.Modules.Exercises.Api.Requests;
internal static class Records
{
    public static WebApplication RegisterRecordsRequests(this WebApplication app)
    {
        app.MapGet("/records", Records.GetAllRexcords)
            .RequireAuthorization()
            .Produces<List<RecordDto>>()
            .Produces(StatusCodes.Status200OK);

        app.MapGet("/records/{id}", Records.GetRecordById)
            .RequireAuthorization()
            .Produces<RecordDto>()
            .Produces(StatusCodes.Status200OK);

        app.MapPost("/records", Records.CreateRecord)
            .RequireAuthorization()
            .Accepts<CreateRecord>("application/json")
            .Produces(StatusCodes.Status201Created);

        app.MapPut("/records", Records.UpdateRecord)
            .RequireAuthorization()
            .Accepts<UpdateRecord>("application/json")
            .Produces(StatusCodes.Status200OK);

        app.MapDelete("/records/{id}", Records.DeleteRecord)
            .RequireAuthorization()
            .Produces(StatusCodes.Status204NoContent);

        return app;
    }

    private static async Task<IResult> GetAllRexcords(IMediator mediator, [AsParameters] GetAllRecordsForUser request)
    {
        var user = await mediator.Send(request);
        return Results.Ok(user);
    }

    private static async Task<IResult> GetRecordById(IMediator mediator, Guid id)
    {
        var request = new GetRecordById(id);
        var user = await mediator.Send(request);
        return Results.Ok(user);
    }

    private static async Task<IResult> CreateRecord(IMediator mediator, [FromBody] CreateRecord request)
    {
        var id = await mediator.Send(request);
        return Results.Created($"/records/{id}", "");
    }

    private static async Task<IResult> UpdateRecord(IMediator mediator, [FromBody] UpdateRecord request)
    {
        await mediator.Send(request);
        return Results.Ok();
    }

    private static async Task<IResult> DeleteRecord(IMediator mediator, Guid id)
    {
        var request = new DeleteRecord(id);
        await mediator.Send(request);
        return Results.NoContent();
    }
}
