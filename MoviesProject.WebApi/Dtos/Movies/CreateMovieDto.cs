namespace MoviesProject.WebApi.Dtos.Movies;

public class CreateMovieDto
{
    public string Title { get; set; }
    public int Episode { get; set; }
    public string Director { get; set; }
    public string Producer { get; set; }
    public string OpenningCrawl { get; set; }
}
