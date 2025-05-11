using MoviesProject.Commons.Abstractions;
using MoviesProject.Commons.Inrastructure.Proxies.Interfaces;
using MoviesProject.Commons.Models;
using MoviesProject.Commons.Shared;

namespace MoviesProject.Commons.Features.Queries.GetAllMovies;

public sealed class GetAllMoviesQueryHandler(
    IStarWarsApiProxy starWarsApiProxy
) : IQueryHandler<GetAllMoviesQuery, GetAllMoviesQueryResponse>
{
    private readonly IStarWarsApiProxy _StarWarsApiProxy = starWarsApiProxy;
    public async Task<Result<GetAllMoviesQueryResponse>> Handle(GetAllMoviesQuery request, CancellationToken cancellationToken)
    {
        var movies = await _StarWarsApiProxy.GetAllMoviesAsync();
        if (movies is null || movies.Movies is null || movies.Movies.Count == 0)
        {
            return Result<GetAllMoviesQueryResponse>.Failure("No movies found.");
        }
        return Result<GetAllMoviesQueryResponse>.Success(new GetAllMoviesQueryResponse(movies.Movies.Select(m => new Movie()
        {
            Title = m.Title,
            Episode = m.Episode,
            OpenningCrawl = m.OpenningCrawl,
        }
        ).ToList()));
    }
}
