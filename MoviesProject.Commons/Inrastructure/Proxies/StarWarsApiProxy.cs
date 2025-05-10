using MoviesProject.Commons.Inrastructure.Proxies.Interfaces;
using MoviesProject.Commons.Inrastructure.Proxies.Models;
using Microsoft.Extensions.Http;
using MoviesProject.Commons.Inrastructure.Proxies.Settings;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

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
        var httpClient = _HttpClientFactory.CreateClient();
        // httpClient.BaseAddress = new Uri(_StarWarsApiProxySettings.BaseUrl);
        var response = await httpClient.GetAsync("https://mx-leal-webapi.bistrosoft.com/api/v1/check");


        var content = await response.Content.ReadAsStringAsync();
        _Logger.LogInformation("Response from StarWarsApi: {Content}", content);
        var result = System.Text.Json.JsonSerializer.Deserialize<GetAllMoviesResponseNetworkEntity>(content);
        return result;

    }
}
