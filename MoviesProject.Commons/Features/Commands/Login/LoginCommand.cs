using MoviesProject.Commons.Abstractions;

namespace MoviesProject.Commons.Features.Commands.Login;

public sealed record LoginCommand(
    string Username,
    string Password
) : ICommand<LoginCommandResponse>;
