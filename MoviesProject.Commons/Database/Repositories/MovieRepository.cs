using Microsoft.EntityFrameworkCore;
using MoviesProject.Commons.Database.Repositories.Interfaces;
using MoviesProject.Commons.Models;

namespace MoviesProject.Commons.Database.Repositories;

public class MovieRepository(ApplicationDbContext context) : IMovieRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<int> AddMovieAsync(Movie movie)
    {
        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();
        return movie.Id;
    }

    public async Task DeleteMovieAsync(Movie movie)
    {
        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Movie>> GetAllMoviesAsync()
    {
        return await _context.Movies.ToListAsync();
    }

    public async Task<Movie> GetMovieByIdAsync(int id)
    {
        return await _context.Movies.Where(m => m.Id == id).FirstOrDefaultAsync();
    }

    public async Task<int> UpdateMovieAsync(Movie movie)
    {
        movie.UpdatedAt = DateTime.UtcNow;
        _context.Movies.Update(movie);
        return await _context.SaveChangesAsync();
    }
}
