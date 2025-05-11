using MoviesProject.Commons.Abstractions;
using MoviesProject.Commons.Database.Repositories.Interfaces;
using MoviesProject.Commons.Inrastructure.Proxies.Interfaces;
using MoviesProject.Commons.Models;
using MoviesProject.Commons.Shared;

namespace MoviesProject.Commons.Features.Queries.GetAllMovies;

public sealed class GetAllMoviesQueryHandler(
    IMovieRepository movieRepository
) : IQueryHandler<GetAllMoviesQuery, GetAllMoviesQueryResponse>
{
    private readonly IMovieRepository _MovieRepository = movieRepository;
    public async Task<Result<GetAllMoviesQueryResponse>> Handle(GetAllMoviesQuery request, CancellationToken cancellationToken)
    {
        var movies = await _MovieRepository.GetAllMoviesAsync();

        return Result<GetAllMoviesQueryResponse>.Success(new GetAllMoviesQueryResponse(movies));
    }
}
