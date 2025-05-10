namespace MoviesProject.Commons.Models;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Episode { get; set; }
    public string OpenningCrawl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
