
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoviesProject.Commons.Models;

namespace MoviesProject.Commons.Database;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }
    public DbSet<Movie> Movies { get; set; }
    // public DbSet<MovieDetail> MovieDetails { get; set; }


}
