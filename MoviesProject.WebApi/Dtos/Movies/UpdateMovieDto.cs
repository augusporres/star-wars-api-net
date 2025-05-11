namespace MoviesProject.WebApi.Dtos.Movies;

public class UpdateMovieDto
{
    public string Title { get; set; }
    public int Episode { get; set; }
    public string Director { get; set; }
    public string Producer { get; set; }
    public string OpenningCrawl { get; set; }
}
