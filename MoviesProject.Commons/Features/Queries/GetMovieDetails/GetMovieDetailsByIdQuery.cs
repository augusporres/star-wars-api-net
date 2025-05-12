using MoviesProject.Commons.Abstractions;
using MoviesProject.Commons.Attributes;

namespace MoviesProject.Commons.Features.Queries.GetMovieDetails;

[AuditLog]
public sealed record GetMovieDetailsByIdQuery(int Id) : IQuery<GetMovieDetailsByIdQueryResponse>;