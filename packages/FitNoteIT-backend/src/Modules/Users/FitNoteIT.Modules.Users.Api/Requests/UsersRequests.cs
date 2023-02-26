using FitNoteIT.Modules.Users.Core.Common.DTO;
using FitNoteIT.Modules.Users.Core.Features.Commands.LoginUser;
using FitNoteIT.Modules.Users.Core.Features.Commands.RegisterUser;
using FitNoteIT.Modules.Users.Core.Features.Queries.GetAllUsers;
using FitNoteIT.Modules.Users.Core.Features.Queries.GetUserById;
using FitNoteIT.Modules.Users.Core.Features.Queries.SelfGetUser;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitNoteIT.Modules.Users.Api.Requests;
internal static class UsersRequests
{
    public static WebApplication RegisterUsersRequests(this WebApplication app)
    {
        app.MapGet("/users", UsersRequests.GetAllUsers)
            .Produces<List<UserDto>>()
            .RequireAuthorization("is-admin");

        app.MapGet("/users/get/{id}", UsersRequests.GetUserById)
            .Produces<UserDto>()
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization("is-admin");

        app.MapGet("/users/me", UsersRequests.SelfGetUser)
            .Produces<UserDto>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization();

        app.MapPost("/users/register", UsersRequests.RegisterUser)
            .Produces(StatusCodes.Status200OK)
            .Accepts<RegisterUser>("application/json")
            .AllowAnonymous();

        app.MapPost("/users/login", UsersRequests.LoginUser)
            .Produces<JwtDto>()
            .Accepts<LoginUser>("application/json")
            .AllowAnonymous();

        return app;
    }

    private static async Task<IResult> GetAllUsers(IMediator mediator, [AsParameters] GetAllUsers request)
    {
        var user = await mediator.Send(request);
        return Results.Ok(user);
    }

    private static async Task<IResult> GetUserById(IMediator mediator, [AsParameters] GetUserById request)
    {

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
}
