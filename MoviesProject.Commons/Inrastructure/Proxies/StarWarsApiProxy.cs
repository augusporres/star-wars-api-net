using MoviesProject.Commons.Inrastructure.Proxies.Interfaces;
using MoviesProject.Commons.Inrastructure.Proxies.Models;
using Microsoft.Extensions.Http;
using MoviesProject.Commons.Inrastructure.Proxies.Settings;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Bogus;

namespace MoviesProject.Commons.Inrastructure.Proxies;

public class StarWarsApiProxy(
    IHttpClientFactory httpClientFactory,
    IOptions<StarWarsApiProxySettings> starWarsApiProxySettings,
    ILogger<StarWarsApiProxy> logger
) : IStarWarsApiProxy
{
    private readonly IHttpClientFactory _HttpClientFactory = httpClientFactory;
    private readonly StarWarsApiProxySettings _StarWarsApiProxySettings = starWarsApiProxySettings.Value;
    private readonly ILogger<StarWarsApiProxy> _Logger = logger;

    public async Task<GetAllMoviesResponseNetworkEntity> GetAllMoviesAsync()
    {
        try
        {
            var httpClient = _HttpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_StarWarsApiProxySettings.BaseUrl);
            var response = await httpClient.GetAsync(_StarWarsApiProxySettings.GetAllFilmsUrl);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                _Logger.LogInformation($"Response from StarWarsApi: {content}");
                var result = JsonSerializer.Deserialize<GetAllMoviesResponseNetworkEntity>(content);
                return result;
            }
            else
            {
                _Logger.LogError($"Error while calling StarWarsApi: {response.StatusCode}");
                throw new Exception($"Error while calling StarWarsApi: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            _Logger.LogError($"{nameof(StarWarsApiProxy)}: {ex.Message}");
            _Logger.LogWarning("Se insertará información mockeada");
            var filmFaker = new Faker<MovieNetworEntity>()
                .RuleFor(x => x.Title, f => f.Lorem.Sentence())
                .RuleFor(x => x.Episode, f => f.Random.Int(1, 6))
                .RuleFor(x => x.OpenningCrawl, f => f.Lorem.Paragraphs(3))
                .RuleFor(x => x.Director, f => f.Name.FullName())
                .RuleFor(x => x.Producer, f => f.Name.FullName());

            var movies = Enumerable.Range(1, 6)
                .Select(episode => filmFaker.Clone()
                .RuleFor(x => x.Episode, _ => episode)
                .Generate())
                .ToList();

            return new GetAllMoviesResponseNetworkEntity
            {
                Count = movies.Count,
                Movies = movies
            };
        }
    }
}
