using System.Windows.Input;
using MoviesProject.Commons.Abstractions;
using MoviesProject.Commons.Database.Repositories.Interfaces;
using MoviesProject.Commons.Inrastructure.Proxies.Interfaces;
using MoviesProject.Commons.Models;
using MoviesProject.Commons.Shared;

namespace MoviesProject.Commons.Features.Commands.SyncMovies;

public sealed class SyncMoviesCommandHandler(
    IStarWarsApiProxy starWarsApiProxy,
    IMovieRepository movieRepository
) : ICommandHandler<SyncMoviesCommand, SyncMoviesCommandResponse>
{
    private readonly IMovieRepository _MovieRepository = movieRepository;
    private readonly IStarWarsApiProxy _StarWarsApiProxy = starWarsApiProxy;
    public async Task<Result<SyncMoviesCommandResponse>> Handle(SyncMoviesCommand request, CancellationToken cancellationToken)
    {
        var movies = await _StarWarsApiProxy.GetAllMoviesAsync();
        if (movies is null || movies.Movies is null || movies.Movies.Count == 0)
        {
            return Result<SyncMoviesCommandResponse>.Failure("No movies found.");
        }
        var moviesInDb = await _MovieRepository.GetAllMoviesAsync();

        if (moviesInDb is null || moviesInDb.Count == 0)
        {
            // add all the movies to the database
        }
        var moviesToUpdate = movies.Movies
            .Where(apiMovie => moviesInDb.Any(dbMovie => dbMovie.Title == apiMovie.Title && dbMovie.Episode == apiMovie.Episode))
            .ToList();

        foreach (var movie in moviesToUpdate)
        {
            var dbMovie = moviesInDb.First(db => db.Title == movie.Title && db.Episode == movie.Episode);
            dbMovie.OpenningCrawl = movie.OpenningCrawl;
            dbMovie.Director = movie.Director;
            dbMovie.Producer = movie.Producer;
            await _MovieRepository.UpdateMovieAsync(dbMovie);
        }

        var moviesToAdd = movies.Movies
            .Where(apiMovie => !moviesInDb.Any(dbMovie => dbMovie.Title == apiMovie.Title && dbMovie.Episode == apiMovie.Episode))
            .Select(apiMovie => new Movie
            {
                Title = apiMovie.Title,
                Episode = apiMovie.Episode,
                OpenningCrawl = apiMovie.OpenningCrawl,
                Director = apiMovie.Director,
                Producer = apiMovie.Producer
            })
            .ToList();

        foreach (var movie in moviesToAdd)
        {
            await _MovieRepository.AddMovieAsync(movie);
        }
        return Result<SyncMoviesCommandResponse>.Success(new SyncMoviesCommandResponse());
    }
}
