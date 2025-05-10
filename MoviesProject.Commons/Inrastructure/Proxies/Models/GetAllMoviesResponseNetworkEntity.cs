using System.Text.Json.Serialization;

namespace MoviesProject.Commons.Inrastructure.Proxies.Models;

public class GetAllMoviesResponseNetworkEntity
{
    public int Count { get; set; }
    public List<MovieNetworEntity>? Results { get; set; }
}

public class MovieNetworEntity
{
    public string Title { get; set; }
    [JsonPropertyName("episode_id")]
    public int Episode { get; set; }
    [JsonPropertyName("opening_crawl")]
    public string OpenningCrawl { get; set; }
    public string Director { get; set; }
    public string Producer { get; set; }
}