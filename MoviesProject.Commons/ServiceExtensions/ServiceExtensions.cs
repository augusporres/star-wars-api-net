using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoviesProject.Commons.Inrastructure.Proxies;
using MoviesProject.Commons.Inrastructure.Proxies.Interfaces;
using MoviesProject.Commons.Inrastructure.Proxies.Settings;

namespace MoviesProject.Commons.ServiceExtensions;

public static class ServiceExtensions
{
    public static void FillSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions();
        services.Configure<StarWarsApiProxySettings>(x => x.BaseUrl = configuration["StarWarsApi:BaseUrl"]);
        services.Configure<StarWarsApiProxySettings>(x => x.GetAllFilmsUrl = configuration["StarWarsApi:GetAllFilmsUrl"]);
    }

    public static void AddProxies(this IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddScoped<IStarWarsApiProxy, StarWarsApiProxy>();
    }
}
