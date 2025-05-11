using MoviesProject.Commons.Abstractions;

namespace MoviesProject.Commons.Features.Queries.GetMovieDetails;

public sealed record GetMovieDetailsByIdQuery(int Id) : IQuery<GetMovieDetailsByIdQueryResponse>;