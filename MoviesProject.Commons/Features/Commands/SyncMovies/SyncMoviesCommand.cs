using MoviesProject.Commons.Abstractions;
using MoviesProject.Commons.Attributes;

namespace MoviesProject.Commons.Features.Commands.SyncMovies;

[AuditLog]
public sealed record SyncMoviesCommand() : ICommand<SyncMoviesCommandResponse>;
