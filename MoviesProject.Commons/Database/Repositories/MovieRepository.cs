using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

    public async Task<Movie> GetMovieByIdAsync(int id)
    {
        return await _context.Movies.Where(m => m.Id == id).FirstOrDefaultAsync();
    }
}
