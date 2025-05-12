using MoviesProject.WebApi.Dtos.Movies;
using Swashbuckle.AspNetCore.Filters;

namespace MoviesProject.WebApi.Examples.CreateMovie;

public class CreateMovieRequestExample : IExamplesProvider<CreateMovieDto>
{
    public CreateMovieDto GetExamples()
    {
        return new CreateMovieDto
        {
            Title = "A New Hope",
            Episode = 4,
            OpenningCrawl = "It is a period of civil war. Rebel spaceships, striking from a hidden base, have won their first victory against the evil Galactic Empire.",
            Director = "George Lucas",
            Producer = "Gary Kurtz, George Lucas"
        };
    }
}
