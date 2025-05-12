namespace MoviesProject.Commons.Features.Commands.Login;

public sealed record LoginCommandResponse(
    string Token,
    string Username,
    List<string> Roles
);
