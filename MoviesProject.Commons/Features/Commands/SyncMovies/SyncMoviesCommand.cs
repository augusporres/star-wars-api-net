using System.Windows.Input;
using MoviesProject.Commons.Abstractions;

namespace MoviesProject.Commons.Features.Commands.SyncMovies;

public sealed record SyncMoviesCommand() : ICommand<SyncMoviesCommandResponse>;
