
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoviesProject.Commons.Database.Configurations;
using MoviesProject.Commons.Models;

namespace MoviesProject.Commons.Database;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new MovieConfiguration());
        // modelBuilder.ApplyConfiguration(new MovieDetailConfiguration());
    }
    public DbSet<Movie> Movies { get; set; }
    // public DbSet<MovieDetail> MovieDetails { get; set; }


}
