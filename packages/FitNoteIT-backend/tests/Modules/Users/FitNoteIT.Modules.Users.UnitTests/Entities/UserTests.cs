using FitNoteIT.Shared.Tests.Seeders;
using FluentAssertions;
using Xunit;

namespace FitNoteIT.Modules.Users.UnitTests.Entities;
public class UserTests
{
    private readonly UserSeeder _seeder;
    public UserTests()
    {
        _seeder = new UserSeeder();
    }

    [Fact]
    public void Verify_ForValidData_ShouldSuccess()
    {
        //Arrange
        var user = _seeder.GetDefaultUser();
        var currentDate = new DateTime(2023, 04, 02, 21, 37, 0);

        //Act
        var exception = Record.Exception(() => user.Verify(currentDate));

        //Assert
        exception.Should().BeNull();
    }

    [Fact]
    public void Verify_ForInvalidData_ShouldFail()
    {
        //Arrange
        var user = _seeder.GetDefaultUser();
        var currentDate = new DateTime(0);

        //Act
        var exception = Record.Exception(() => user.Verify(currentDate));

        //Assert
        exception.Should().NotBeNull();
    }

    [Fact]
    public void Verify_ForAlreadyVerified_ShouldFail()
    {
        //Arrange
        var user = _seeder.GetDefaultUser();
        var currentDate = new DateTime(2023, 04, 02, 21, 37, 0);

        //Act
        user.Verify(currentDate);
        var exception = Record.Exception(() => user.Verify(currentDate));

        //Assert
        exception.Should().NotBeNull();
    }

    [Fact]
    public void SetRefreshToken_ForValidData_ShouldSuccess()
    {
        //Arrange
        var user = _seeder.GetDefaultUser();
        var refreshToken = "+BFs4/6wjGFTHI7glPGuoQXQNLTuKk+pH4m712RrNew=";

        //Act
        var exception = Record.Exception(() => user.SetRefreshToken(refreshToken));

        //Assert
        exception.Should().BeNull();
    }

    [Fact]
    public void SetRefreshToken_ForInvalidData_ShouldSuccess()
    {
        //Arrange
        var user = _seeder.GetDefaultUser();
        var refreshToken = "";

        //Act
        var exception = Record.Exception(() => user.SetRefreshToken(refreshToken));

        //Assert
        exception.Should().NotBeNull();
    }

    [Fact]
    public void SetRefreshTokenExpiryTime_ForValidData_ShouldSuccess()
    {
        //Arrange
        var user = _seeder.GetDefaultUser();
        var currentDate = DateTime.UtcNow;

        //Act
        var exception = Record.Exception(() => user.SetRefreshTokenExpiryTime(currentDate));

        //Assert
        exception.Should().BeNull();
    }

    [Fact]
    public void SetRefreshTokenExpiryTime_ForInvalidData_ShouldSuccess()
    {
        //Arrange
        var user = _seeder.GetDefaultUser();
        var currentDate = new DateTime(0);

        //Act
        var exception = Record.Exception(() => user.SetRefreshTokenExpiryTime(currentDate));

        //Assert
        exception.Should().NotBeNull();
    }

    public static readonly object[][] IsTokenValidCorrectData =
    {
        new object[] { "+BFs4/6wjGFTHI7glPGuoQXQNLTuKk+pH4m712RrNew=", new DateTime(2023, 04, 02, 21, 37, 0) },
    };

    [Theory, MemberData(nameof(IsTokenValidCorrectData))]
    public void IsTokenValid_ForValidData_ShouldSuccess(string refreshToken, DateTime date)
    {
        //Arrange
        var user = _seeder.GetDefaultUser();

        var currentRefreshToken = "+BFs4/6wjGFTHI7glPGuoQXQNLTuKk+pH4m712RrNew=";
        var currentExpiryDate = new DateTime(2023, 04, 02, 21, 37, 30);

        //Act
        user.SetRefreshToken(currentRefreshToken);
        user.SetRefreshTokenExpiryTime(currentExpiryDate);
        var test1 = currentRefreshToken == refreshToken;
        var test2 = currentExpiryDate >= date;
        var result = user.IsTokenValid(refreshToken, date);

        //Assert
        result.Should().BeTrue();
    }

    public static readonly object[][] IsTokenValidIncorrectData =
    {
        new object[] { "+BFs4/6wjGFTHI7glPGuoQXQNLTuKk+pH4m712RrNew=", new DateTime(2023, 04, 02, 21, 37, 0) },
        new object[] { "QXFQN/FByFhDzHTS3Wx/NAG43RA29/YB2G9rn2lX4IA==", new DateTime(2023, 04, 02, 21, 37, 50) },
        new object[] { "+BFs4/6wjGFTHI7glPGuoQXQNLTuKk+pH4m712RrNew=", new DateTime(2023, 04, 02, 21, 37, 50) },
    };

    [Theory, MemberData(nameof(IsTokenValidIncorrectData))]
    public void IsTokenValid_ForInvalidData_ShouldSuccess(string refreshToken, DateTime date)
    {
        //Arrange
        var user = _seeder.GetDefaultUser();

        var currentRefreshToken = "QXFQN/FByFhDzHTS3Wx/NAG43RA29/YB2G9rn2lX4IA=";
        var currentExpiryDate = new DateTime(2023, 04, 02, 21, 37, 30);

        //Act
        user.SetRefreshToken(currentRefreshToken);
        user.SetRefreshTokenExpiryTime(currentExpiryDate);
        var test1 = currentRefreshToken == refreshToken;
        var test2 = currentExpiryDate <= date;
        var result = user.IsTokenValid(refreshToken, date);

        //Assert
        result.Should().BeFalse();
    }
}
