using FitNoteIT.Modules.Users.Core.Features.Commands.RegisterUser;
using FitNoteIT.Modules.Users.Core.Persistence.Contexts;
using FitNoteIT.Shared.Tests;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace FitNoteIT.Modules.Users.IntegrationTests.Requests;
public class UserRequests : TestRequests
{
    public UserRequests() : base()
    {

    }

    protected override void ConfigureServices(IServiceCollection services)
    {
        var dbContextOptions = services
                              .SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<UsersDbContext>));

        services.Remove(dbContextOptions);

        services
         .AddDbContext<UsersDbContext>(options => options.UseInMemoryDatabase("FitNoteIT"));
    }

    [Fact]
    public async Task Post_RegisterUser_ShouldReturn200OK()
    {
        var command = new RegisterUser("testuser@test.com", "testuser", "password123", "password123");
        var response = await Client.PostAsJsonAsync("users/register", command);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}