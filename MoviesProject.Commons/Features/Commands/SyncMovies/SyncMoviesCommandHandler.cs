using System.Windows.Input;
using Microsoft.Extensions.Logging;
using MoviesProject.Commons.Abstractions;
using MoviesProject.Commons.Database.Repositories.Interfaces;
using MoviesProject.Commons.Inrastructure.Proxies.Interfaces;
using MoviesProject.Commons.Models;
using MoviesProject.Commons.Shared;

namespace MoviesProject.Commons.Features.Commands.SyncMovies;

public sealed class SyncMoviesCommandHandler(
    IStarWarsApiProxy starWarsApiProxy,
    IMovieRepository movieRepository,
    ILogger<SyncMoviesCommandHandler> logger
) : ICommandHandler<SyncMoviesCommand, SyncMoviesCommandResponse>
{
    private readonly IMovieRepository _MovieRepository = movieRepository;
    private readonly IStarWarsApiProxy _StarWarsApiProxy = starWarsApiProxy;
    private readonly ILogger<SyncMoviesCommandHandler> _Logger = logger;
    public async Task<Result<SyncMoviesCommandResponse>> Handle(SyncMoviesCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var moviesFromApi = await _StarWarsApiProxy.GetAllMoviesAsync();
            if (moviesFromApi?.Movies?.Count == 0)
            {
                return Result<SyncMoviesCommandResponse>.Failure("No movies found.");
            }
            var moviesInDb = await _MovieRepository.GetAllMoviesAsync();

            var dbMovieMap = moviesInDb.ToDictionary(m => (m.Title, m.Episode), m => m);

            var moviesToAdd = new List<Movie>();
            var moviesToUpdate = new List<Movie>();

            foreach (var apiMovie in moviesFromApi.Movies)
            {
                if (dbMovieMap.TryGetValue((apiMovie.Title, apiMovie.Episode), out var dbMovie))
                {
                    dbMovie.OpenningCrawl = apiMovie.OpenningCrawl;
                    dbMovie.Director = apiMovie.Director;
                    dbMovie.Producer = apiMovie.Producer;
                    moviesToUpdate.Add(dbMovie);
                }
                else
                {
                    moviesToAdd.Add(new Movie
                    {
                        Title = apiMovie.Title,
                        Episode = apiMovie.Episode,
                        OpenningCrawl = apiMovie.OpenningCrawl,
                        Director = apiMovie.Director,
                        Producer = apiMovie.Producer
                    });

                }
            }



            if (moviesToAdd.Count > 0)
            {
                await _MovieRepository.AddMoviesAsync(moviesToAdd);
            }
            if (moviesToUpdate.Count > 0)
            {
                await _MovieRepository.UpdateMoviesAsync(moviesToUpdate);
            }
            return Result<SyncMoviesCommandResponse>.Success(new SyncMoviesCommandResponse());
        }
        catch (Exception ex)
        {
            _Logger.LogError($"{nameof(SyncMoviesCommandHandler)}: {ex.Message}");
            return Result<SyncMoviesCommandResponse>.Failure("Ocurrió un error al sincronizar las películas.");
        }
    }
}
