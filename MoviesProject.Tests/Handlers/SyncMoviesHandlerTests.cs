using Microsoft.Extensions.Logging;
using MoviesProject.Commons.Database.Repositories.Interfaces;
using MoviesProject.Commons.Features.Commands.SyncMovies;
using MoviesProject.Commons.Inrastructure.Proxies.Interfaces;
using MoviesProject.Commons.Inrastructure.Proxies.Models;
using MoviesProject.Commons.Models;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace MoviesProject.Tests.Handlers;

public class SyncMoviesHandlerTests
{
    private readonly IMovieRepository _movieRepositoryMock = Substitute.For<IMovieRepository>();
    private readonly IStarWarsApiProxy _starWarsApiProxyMock = Substitute.For<IStarWarsApiProxy>();
    private readonly ILogger<SyncMoviesCommandHandler> _loggerMock = Substitute.For<ILogger<SyncMoviesCommandHandler>>();
    private readonly SyncMoviesCommandHandler _handler;

    public SyncMoviesHandlerTests()
    {
        _handler = new SyncMoviesCommandHandler(_starWarsApiProxyMock, _movieRepositoryMock, _loggerMock);
    }


    [Fact]
    public async Task ShouldAddAndUpdateMovies_WhenMoviesExistInApi()
    {
        // Arrange
        var apiMovies = new List<MovieNetworEntity>
        {
            new MovieNetworEntity { Title = "Movie 1", Episode = 1, OpenningCrawl = "Crawl 1", Director = "Director 1", Producer = "Producer 1" },
            new MovieNetworEntity { Title = "Movie 2", Episode = 2, OpenningCrawl = "Crawl 2", Director = "Director 2", Producer = "Producer 2" }
        };

        var dbMovies = new List<Movie>
        {
            new Movie { Title = "Movie 1", Episode = 1, OpenningCrawl = "Old Crawl", Director = "Old Director", Producer = "Old Producer" }
        };

        _starWarsApiProxyMock.GetAllMoviesAsync().Returns(new GetAllMoviesResponseNetworkEntity { Movies = apiMovies });
        _movieRepositoryMock.GetAllMoviesAsync().Returns(dbMovies);

        var command = new SyncMoviesCommand();

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        await _movieRepositoryMock.Received(1).AddMoviesAsync(Arg.Is<List<Movie>>(movies => movies.Count == 1 && movies[0].Title == "Movie 2"));
        await _movieRepositoryMock.Received(1).UpdateMoviesAsync(Arg.Is<List<Movie>>(movies => movies.Count == 1 && movies[0].OpenningCrawl == "Crawl 1"));
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenNoMoviesExistInApi()
    {
        // Arrange
        _starWarsApiProxyMock.GetAllMoviesAsync().Returns(new GetAllMoviesResponseNetworkEntity { Movies = new List<MovieNetworEntity>() });

        var command = new SyncMoviesCommand();

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("No movies found.", result.Error);
        await _movieRepositoryMock.DidNotReceive().AddMoviesAsync(Arg.Any<List<Movie>>());
        await _movieRepositoryMock.DidNotReceive().UpdateMoviesAsync(Arg.Any<List<Movie>>());
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        // Arrange
        _starWarsApiProxyMock.GetAllMoviesAsync().Throws(new Exception("API error"));

        var command = new SyncMoviesCommand();

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Ocurrió un error al sincronizar las películas.", result.Error);
    }
}
