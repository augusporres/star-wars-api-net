using MoviesProject.Commons.Abstractions;
using MoviesProject.Commons.Attributes;

namespace MoviesProject.Commons.Features.Commands.CreateMovie;

[AuditLog]
public sealed record CreateMovieCommand(
    string Title,
    int EpisodeId,
    string Director,
    string Producer,
    string OpenningCrawl
) : ICommand<CreateMovieCommandResponse>;