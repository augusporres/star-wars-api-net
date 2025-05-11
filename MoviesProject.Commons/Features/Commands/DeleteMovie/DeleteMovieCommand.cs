using System.Windows.Input;
using MoviesProject.Commons.Abstractions;

namespace MoviesProject.Commons.Features.Commands.DeleteMovie;

public sealed record DeleteMovieCommand(int Id) : ICommand<DeleteMovieCommandResponse>;
