using System.Windows.Input;
using MoviesProject.Commons.Abstractions;

namespace MoviesProject.Commons.Features.Commands.RegisterUser;

public sealed record RegisterUserCommand(
    string Username,
    string Password
) : ICommand<RegisterUserCommandResponse>;