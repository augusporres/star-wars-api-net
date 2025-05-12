using MoviesProject.Commons.Models;

namespace MoviesProject.Commons.Database.Repositories.Interfaces;

public interface IMovieRepository
{
    Task<List<Movie>> GetAllMoviesAsync();
    Task<Movie> GetMovieByIdAsync(int id);
    Task<int> AddMovieAsync(Movie movie);
    Task AddMoviesAsync(List<Movie> movies);
    Task<int> UpdateMovieAsync(Movie movie);
    Task UpdateMoviesAsync(List<Movie> movies);
    Task DeleteMovieAsync(Movie movie);
}
