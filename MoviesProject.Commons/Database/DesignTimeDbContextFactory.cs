using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


namespace MoviesProject.Commons.Database;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddUserSecrets("dotnet-Movies.WebApi")
            .Build();

        var connectionString = configuration.GetConnectionString("MainConnection");
        optionsBuilder.UseNpgsql(connectionString, b => b.MigrationsAssembly("MoviesProject.WebApi"));

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}