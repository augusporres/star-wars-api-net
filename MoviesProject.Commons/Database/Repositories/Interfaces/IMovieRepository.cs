using MoviesProject.Commons.Models;

namespace MoviesProject.Commons.Database.Repositories.Interfaces;

public interface IMovieRepository
{
    Task<List<Movie>> GetAllMoviesAsync();
    Task<Movie> GetMovieByIdAsync(int id);
    Task<int> AddMovieAsync(Movie movie);
    Task<int> UpdateMovieAsync(Movie movie);
    Task DeleteMovieAsync(Movie movie);
}
