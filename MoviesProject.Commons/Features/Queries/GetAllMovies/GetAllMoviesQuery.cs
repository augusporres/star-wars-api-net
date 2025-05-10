using MoviesProject.Commons.Abstractions;

namespace MoviesProject.Commons.Features.Queries.GetAllMovies;

public sealed record class GetAllMoviesQuery() : IQuery<GetAllMoviesQueryResponse>;
