using MoviesProject.Commons.Abstractions;
using MoviesProject.Commons.Attributes;

namespace MoviesProject.Commons.Features.Queries.GetAllMovies;

[AuditLog]
public sealed record class GetAllMoviesQuery() : IQuery<GetAllMoviesQueryResponse>;
