using MoviesProject.Commons.Models;
using Swashbuckle.AspNetCore.Filters;

namespace MoviesProject.WebApi.Examples.GetAllMovies;

public class GetAllMoviesSucessResponseExample : IExamplesProvider<List<Movie>>
{
    public List<Movie> GetExamples()
    {
        return new List<Movie>
        {
            new Movie
            {
                Title = "A New Hope",
                Episode = 4,
                OpenningCrawl = "It is a period of civil war. Rebel spaceships, striking from a hidden base, have won their first victory against the evil Galactic Empire.",
                Director = "George Lucas",
                Producer = "Gary Kurtz, George Lucas"
            },
            new Movie
            {
                Title = "The Empire Strikes Back",
                Episode = 5,
                OpenningCrawl = "It is a dark time for the Rebellion. Although the Death Star has been destroyed, Imperial troops have driven the Rebel forces from their hidden base and pursued them across the galaxy.",
                Director = "Irvin Kershner",
                Producer = "Gary Kurtz, George Lucas"
            }
        };
    }
}
