using System.Windows.Input;
using MoviesProject.Commons.Abstractions;

namespace MoviesProject.Commons.Features.Commands.CreateMovie;

public sealed record CreateMovieCommand(
    string Title,
    int EpisodeId,
    string Director,
    string Producer,
    string OpenningCrawl
) : ICommand<CreateMovieCommandResponse>;