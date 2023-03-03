using FitNoteIT.Modules.Users.Core.Common.DTO;
using FitNoteIT.Modules.Users.Core.Features.Commands.LoginUser;
using FitNoteIT.Modules.Users.Core.Features.Commands.RegisterUser;
using FitNoteIT.Modules.Users.Core.Features.Commands.TokenRefresh;
using FitNoteIT.Modules.Users.Core.Features.Commands.TokenRemove;
using FitNoteIT.Modules.Users.Core.Features.Queries.GetAllUsers;
using FitNoteIT.Modules.Users.Core.Features.Queries.GetUserById;
using FitNoteIT.Modules.Users.Core.Features.Queries.SelfGetUser;
using FitNoteIT.Shared.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitNoteIT.Modules.Users.Api.Requests;
internal static class Users
{
    public static WebApplication RegisterUsersRequests(this WebApplication app)
    {
        app.MapGet("/users", Users.GetAllUsers)
            .RequireAuthorization("is-admin")
            .Produces<List<UserDto>>()
            .Produces(StatusCodes.Status400BadRequest);

        app.MapGet("/users/{id}", Users.GetUserById)
            .RequireAuthorization("is-admin")
            .Produces<UserDto>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);

        app.MapGet("/users/me", Users.SelfGetUser)
            .RequireAuthorization()
            .Produces<UserDto>()
            .Produces(StatusCodes.Status400BadRequest);

        app.MapPost("/users/register", Users.RegisterUser)
            .AllowAnonymous()
            .Accepts<RegisterUser>("application/json")
            .Produces(StatusCodes.Status400BadRequest);

        app.MapPost("/users/login", Users.LoginUser)
            .AllowAnonymous()   
            .Accepts<LoginUser>("application/json")
            .Produces<TokensDto>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);

        app.MapPost("/token/refresh", Users.TokenRefresh)
            .AllowAnonymous()
            .Accepts<TokenRefresh>("application/json")
            .Produces(StatusCodes.Status400BadRequest);
            
        app.MapPatch("/token/remove", Users.TokenRemove)
            .RequireAuthorization()
            .Produces(StatusCodes.Status400BadRequest);

        return app;
    }

    private static async Task<IResult> GetAllUsers(IMediator mediator, [AsParameters] GetAllUsers request)
    {
        var user = await mediator.Send(request);
        return Results.Ok(user);
    }

    private static async Task<IResult> GetUserById(IMediator mediator, string input)
    {
        if (!Guid.TryParse(input, out Guid id)) throw new BadRequestException("Invalid GUID format");

        var request = new GetUserById(id);
        var user = await mediator.Send(request);
        return Results.Ok(user);
    }

    private static async Task<IResult> SelfGetUser(IMediator mediator)
    {
        var user = await mediator.Send(new SelfGetUser());
        return Results.Ok(user);
    }

    private static async Task<IResult> RegisterUser(IMediator mediator, [FromBody] RegisterUser request)
    {
        await mediator.Send(request);
        return Results.Ok();
    }

    private static async Task<IResult> LoginUser(IMediator mediator, [FromBody] LoginUser request)
    {
        var token = await mediator.Send(request);
        return Results.Ok(token);
    }

    private static async Task<IResult> TokenRefresh(IMediator mediator, [FromBody] TokenRefresh request)
    {
        var token = await mediator.Send(request);
        return Results.Ok(token);
    }

    private static async Task<IResult> TokenRemove(IMediator mediator)
    {
        await mediator.Send(new TokenRemove());
        return Results.NoContent();
    }
}
