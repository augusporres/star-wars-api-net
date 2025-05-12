using System.Windows.Input;
using MoviesProject.Commons.Abstractions;
using MoviesProject.Commons.Attributes;

namespace MoviesProject.Commons.Features.Commands.DeleteMovie;

[AuditLog]
public sealed record DeleteMovieCommand(int Id) : ICommand<DeleteMovieCommandResponse>;
