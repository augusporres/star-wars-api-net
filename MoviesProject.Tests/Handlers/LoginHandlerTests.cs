using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MoviesProject.Commons.Features.Commands.Login;
using MoviesProject.Commons.Models;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace MoviesProject.Tests.Handlers;

public class LoginCommandHandlerTests
{
    private readonly UserManager<User> _userManagerMock;
    private readonly SignInManager<User> _signInManagerMock;
    private readonly IConfiguration _configurationMock;
    private readonly ILogger<LoginCommandHandler> _loggerMock = Substitute.For<ILogger<LoginCommandHandler>>();
    private readonly LoginCommandHandler _handler;

    public LoginCommandHandlerTests()
    {
        _userManagerMock = Substitute.For<UserManager<User>>(
            Substitute.For<IUserStore<User>>(),
            null, null, null, null, null, null, null, null
        );

        _signInManagerMock = Substitute.For<SignInManager<User>>(
            _userManagerMock,
            Substitute.For<IHttpContextAccessor>(),
            Substitute.For<IUserClaimsPrincipalFactory<User>>(),
            null, null, null, null
        );

        _configurationMock = Substitute.For<IConfiguration>();
        _configurationMock["JWT:Key"].Returns("YourSuperSecretKeyThatIsAtLeast16Chars");
        _configurationMock["JWT:Issuer"].Returns("TestIssuer");
        _configurationMock["JWT:Audience"].Returns("TestAudience");

        _handler = new LoginCommandHandler(_userManagerMock, _signInManagerMock, _configurationMock, _loggerMock);
    }

    [Fact]
    public async Task Handle_ShouldReturnToken_WhenCredentialsAreValid()
    {
        // Arrange
        var user = new User { Id = "1", UserName = "testuser" };
        var roles = new List<string> { "Admin" };
        var command = new LoginCommand("testuser", "testPassword3!");

        _userManagerMock.FindByNameAsync(command.Username).Returns(user);
        _signInManagerMock.CheckPasswordSignInAsync(user, command.Password, false).Returns(SignInResult.Success);
        _userManagerMock.GetRolesAsync(user).Returns(roles);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal("testuser", result.Value.Username);
        Assert.Single(result.Value.Roles);
        Assert.Contains("Admin", result.Value.Roles);

        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(result.Value.Token);
        Assert.Equal("testuser", token.Claims.First(c => c.Type == ClaimTypes.Name).Value);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenUserNotFound()
    {
        // Arrange
        var command = new LoginCommand("nonexistentuser", "password123");
        _userManagerMock.FindByNameAsync(command.Username).Returns((User?)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Usuario no encontrado", result.Error);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenPasswordIsIncorrect()
    {
        // Arrange
        var user = new User { Id = "1", UserName = "testuser" };
        var command = new LoginCommand("testuser", "wrongpassword");

        _userManagerMock.FindByNameAsync(command.Username).Returns(user);
        _signInManagerMock.CheckPasswordSignInAsync(user, command.Password, false).Returns(SignInResult.Failed);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Contraseña incorrecta", result.Error);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        // Arrange
        var command = new LoginCommand("testuser", "password123");
        _userManagerMock.FindByNameAsync(command.Username).Throws(new Exception("Unexpected error"));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Error al procesar la solicitud de inicio de sesión", result.Error);
    }
}
