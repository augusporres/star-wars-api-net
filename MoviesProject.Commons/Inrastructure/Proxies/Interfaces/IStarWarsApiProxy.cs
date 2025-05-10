using MoviesProject.Commons.Inrastructure.Proxies.Models;

namespace MoviesProject.Commons.Inrastructure.Proxies.Interfaces;

public interface IStarWarsApiProxy
{
    Task<GetAllMoviesResponseNetworkEntity> GetAllMoviesAsync();
}
