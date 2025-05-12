using MoviesProject.Commons.Abstractions;
using MoviesProject.Commons.Attributes;

namespace MoviesProject.Commons.Features.Commands.UpdateMovie;

[AuditLog]
public sealed record UpdateMovieCommand(
    int id,
    string Title,
    int EpisodeId,
    string OpenningCrawl,
    string Director,
    string Producer
) : ICommand<UpdateMovieCommandResponse>;
