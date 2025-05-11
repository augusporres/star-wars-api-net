using System.Windows.Input;
using MoviesProject.Commons.Abstractions;

namespace MoviesProject.Commons.Features.Commands.UpdateMovie;

public sealed record UpdateMovieCommand(
    int id,
    string Title,
    int EpisodeId,
    string OpenningCrawl,
    string Director,
    string Producer
) : ICommand<UpdateMovieCommandResponse>;
