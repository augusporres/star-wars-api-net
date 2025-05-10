using MoviesProject.Commons.Models;

namespace MoviesProject.Commons.Features.Queries.GetAllMovies;

public sealed record GetAllMoviesQueryResponse(
    List<Movie> Movies
);
