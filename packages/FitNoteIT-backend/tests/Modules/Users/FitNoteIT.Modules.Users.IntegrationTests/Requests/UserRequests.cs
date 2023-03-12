using FitNoteIT.Modules.Users.Core.Abstractions.Factories;
using FitNoteIT.Modules.Users.Core.Entities;
using FitNoteIT.Modules.Users.Core.Factories;
using FitNoteIT.Modules.Users.Core.Features.Commands.LoginUser;
using FitNoteIT.Modules.Users.Core.Features.Commands.RegisterUser;
using FitNoteIT.Modules.Users.Core.Features.Commands.TokenRefresh;
using FitNoteIT.Modules.Users.Core.Security;
using FitNoteIT.Modules.Users.Shared.DTO;
using FitNoteIT.Shared.Tests;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace FitNoteIT.Modules.Users.IntegrationTests.Requests;
public class UserRequests : TestRequests, IDisposable
{
    private readonly TestUsersDatabase _testDatabase;
    private readonly IUserFactory _userFactory;
    public UserRequests() : base()
    {
        _testDatabase = new TestUsersDatabase();
        _userFactory = new UserFactory();
    }

    public void Dispose()
    {
        _testDatabase.Dispose();
    }

    protected override void ConfigureServices(IServiceCollection services)
    {

    }

    [Fact]
    public async Task RegisterUser_ReturnsOk()
    {
        //Arrange
        await _testDatabase.Context.Database.MigrateAsync();
        var command = new RegisterUser("testuser@test.com", "testuser", "password123", "password123");

        //Act
        var response = await Client.PostAsJsonAsync("users/register", command);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task LoginUser_ForValidData_ReturnsOk()
    {
        //Arrange
        await _testDatabase.Context.Database.MigrateAsync();
        SeedUser();

        //Act
        var command = new LoginUser("test", "testPassword");
        var response = await Client.PostAsJsonAsync("users/login", command);
        var tokens = await response.Content.ReadFromJsonAsync<TokensDto>();

        //Assert
        tokens.Should().NotBeNull();
        tokens.AccessToken.Should().NotBeNullOrWhiteSpace();
        tokens.RefreshToken.Should().NotBeNullOrWhiteSpace();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task LoginUser_ForInvalidPassword_ReturnsBadRequest()
    {
        //Arrange
        await _testDatabase.Context.Database.MigrateAsync();
        SeedUser();

        //Act
        var command = new LoginUser("test", "randompassword123");      
        var response = await Client.PostAsJsonAsync("users/login", command);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }


    [Fact]
    public async Task LoginUser_ForUnknownUser_ReturnsNotFound()
    {
        //Arrange
        await _testDatabase.Context.Database.MigrateAsync();
        SeedUser();

        //Act
        var command = new LoginUser("test123", "randompassword123");
        var response = await Client.PostAsJsonAsync("users/login", command);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task TokenRefresh_ReturnsOk()
    {
        //Arrange
        await _testDatabase.Context.Database.MigrateAsync();
        SeedUser();
        var loginCommand = new LoginUser("test", "testPassword");
        var loginResponse = await Client.PostAsJsonAsync("users/login", loginCommand);
        var loginTokens = await loginResponse.Content.ReadFromJsonAsync<TokensDto>();

        //Act
        var command = new TokenRefresh(loginTokens.AccessToken, loginTokens.RefreshToken);
        var response = await Client.PostAsJsonAsync("token/refresh", command);
        var tokens = await response.Content.ReadFromJsonAsync<TokensDto>();

        //Assert
        tokens.Should().NotBeNull();
        tokens.AccessToken.Should().NotBeNullOrWhiteSpace();
        tokens.RefreshToken.Should().NotBeNullOrWhiteSpace();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    private void SeedUser()
    {
        var role = _testDatabase.Context.Roles.Single(x => x.Name == "User");
        var passwordManager = new PasswordManager(new PasswordHasher<User>());
        var hashedPassword = passwordManager.Secure("testPassword");
        var user = _userFactory.Create(Guid.NewGuid(), "test@test.com", hashedPassword, "test", DateTime.UtcNow, role);
        _testDatabase.Context.Users.Add(user);
        _testDatabase.Context.SaveChanges();
    }
}
