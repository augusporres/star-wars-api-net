using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MoviesProject.Commons.Features.Commands.RegisterUser;
using MoviesProject.Commons.Models;
using NSubstitute;
using Xunit;

namespace MoviesProject.Tests.Handlers;

public class RegisterUserHandlerTests
{
    private readonly UserManager<User> _userManagerMock;
    private readonly RoleManager<IdentityRole> _roleManagerMock;
    private readonly RegisterUserCommandHandler _handler;

    public RegisterUserHandlerTests()
    {
        _userManagerMock = Substitute.For<UserManager<User>>(
            Substitute.For<IUserStore<User>>(),
            null, null, null, null, null, null, null, null
        );

        _roleManagerMock = Substitute.For<RoleManager<IdentityRole>>(
            Substitute.For<IRoleStore<IdentityRole>>(),
            null, null, null, null
        );

        _handler = new RegisterUserCommandHandler(_userManagerMock, _roleManagerMock);
    }

    [Fact]
    public async Task Handle_ShouldRegisterUser_WhenDataIsValid()
    {
        // Arrange
        var command = new RegisterUserCommand("testuser@example.com", "Password123!");

        var identityResult = IdentityResult.Success;
        _userManagerMock.CreateAsync(Arg.Any<User>(), command.Password).Returns(identityResult);
        _roleManagerMock.RoleExistsAsync("User").Returns(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("Usuario registrado exitosamente", result?.Value?.Message);
        await _userManagerMock.Received(1).CreateAsync(Arg.Is<User>(u => u.UserName == command.Username && u.Email == command.Username), command.Password);
        await _userManagerMock.Received(1).AddToRoleAsync(Arg.Any<User>(), "User");
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenUserCreationFails()
    {
        // Arrange
        var command = new RegisterUserCommand("testuser@example.com", "Password123!");

        var identityResult = IdentityResult.Failed(new IdentityError { Description = "Password is too weak" });
        _userManagerMock.CreateAsync(Arg.Any<User>(), command.Password).Returns(identityResult);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Password is too weak", result.Error);
        await _userManagerMock.Received(1).CreateAsync(Arg.Is<User>(u => u.UserName == command.Username && u.Email == command.Username), command.Password);
        await _userManagerMock.DidNotReceive().AddToRoleAsync(Arg.Any<User>(), Arg.Any<string>());
    }

    [Fact]
    public async Task Handle_ShouldNotAddRole_WhenRoleDoesNotExist()
    {
        // Arrange
        var command = new RegisterUserCommand
        ("testuser@example.com", "Password123!");

        var identityResult = IdentityResult.Success;
        _userManagerMock.CreateAsync(Arg.Any<User>(), command.Password).Returns(identityResult);
        _roleManagerMock.RoleExistsAsync("User").Returns(false);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("Usuario registrado exitosamente", result?.Value?.Message);
        await _userManagerMock.Received(1).CreateAsync(Arg.Is<User>(u => u.UserName == command.Username && u.Email == command.Username), command.Password);
        await _userManagerMock.DidNotReceive().AddToRoleAsync(Arg.Any<User>(), "User");
    }
}
