using Swashbuckle.AspNetCore.Filters;

namespace MoviesProject.WebApi.Examples.SyncMovies;

public class SyncMoviesSuccessResponseExample : IExamplesProvider<object>
{
    public object GetExamples()
    {
        return new { Message = "Películas sincronizadas correctamente" };
    }
}
