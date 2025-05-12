using MoviesProject.Commons.Abstractions;
using MoviesProject.Commons.Attributes;

namespace MoviesProject.Commons.Features.Commands.UpdateMovie;

[AuditLog]
public sealed record UpdateMovieCommand(
    int Id,
    string Title,
    int EpisodeId,
    string OpenningCrawl,
    string Director,
    string Producer
) : ICommand<UpdateMovieCommandResponse>;
