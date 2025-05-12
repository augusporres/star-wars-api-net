using Swashbuckle.AspNetCore.Filters;

namespace MoviesProject.WebApi.Examples.DeleteMovie;

public class DeleteMovieSuccessResponseExample : IExamplesProvider<int>
{
    public int GetExamples()
    {
        return 1;
    }
}
