using MoviesProject.WebApi.Dtos.Movies;
using Swashbuckle.AspNetCore.Filters;

namespace MoviesProject.WebApi.Examples.UpdateMovie;

public class UpdateMovieRequestExample : IExamplesProvider<UpdateMovieDto>
{
    public UpdateMovieDto GetExamples()
    {
        return new UpdateMovieDto
        {
            Title = "A New Hope",
            Episode = 4,
            OpenningCrawl = "It is a period of civil war. Rebel spaceships, striking from a hidden base, have won their first victory against the evil Galactic Empire.",
            Director = "George Lucas",
            Producer = "Gary Kurtz, George Lucas"
        };
    }
}
