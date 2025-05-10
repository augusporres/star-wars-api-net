using MoviesProject.Commons.Abstractions;
using MoviesProject.Commons.Inrastructure.Proxies.Interfaces;
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
        return Result<GetAllMoviesQueryResponse>.Success(new GetAllMoviesQueryResponse(new List<Models.Movie>() { }));
    }
}
