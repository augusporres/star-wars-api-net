using MoviesProject.Commons.Models;

namespace MoviesProject.Commons.Database.Repositories.Interfaces;

public interface IMovieRepository
{
    Task<Movie> GetMovieByIdAsync(int id);
    Task<int> AddMovieAsync(Movie movie);
}
