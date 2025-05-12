using MoviesProject.Commons.Models;
using Swashbuckle.AspNetCore.Filters;

namespace MoviesProject.WebApi.Examples.GetMovieById;

public class GetMovieByIdSuccessResponseExample : IExamplesProvider<Movie>
{
    public Movie GetExamples()
    {
        return new Movie
        {
            Title = "A New Hope",
            Episode = 4,
            OpenningCrawl = "It is a period of civil war. Rebel spaceships, striking from a hidden base, have won their first victory against the evil Galactic Empire.",
            Director = "George Lucas",
            Producer = "Gary Kurtz, George Lucas",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
}
