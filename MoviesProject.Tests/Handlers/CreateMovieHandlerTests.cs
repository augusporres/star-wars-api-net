using Microsoft.Extensions.Logging;
using MoviesProject.Commons.Database.Repositories.Interfaces;
using MoviesProject.Commons.Features.Commands.CreateMovie;
using MoviesProject.Commons.Models;
using NSubstitute;

namespace MoviesProject.Tests.Handlers;

public class CreateMovieHandlerTests
{
    private readonly IMovieRepository _movieRepositoryMock = Substitute.For<IMovieRepository>();
    private readonly ILogger<CreateMovieCommandHandler> _loggerMock = Substitute.For<ILogger<CreateMovieCommandHandler>>();
    private readonly CreateMovieCommandHandler _handler;
    public CreateMovieHandlerTests()
    {
        _handler = new CreateMovieCommandHandler(_movieRepositoryMock, _loggerMock);
    }
    [Fact]
    public async Task Should_Create_Movie_Ok()
    {
        var request = new CreateMovieCommand(
            Title: "A New Hope",
            EpisodeId: 4,
            OpenningCrawl: "It is a period of civil war. Rebel spaceships, striking from a hidden base, have won their first victory against the evil Galactic Empire.",
            Director: "George Lucas",
            Producer: "Gary Kurtz"
        );
        _movieRepositoryMock.GetAllMoviesAsync().Returns(new List<Movie>());
        _movieRepositoryMock.AddMovieAsync(Arg.Any<Movie>()).Returns(1);
        var result = await _handler.Handle(request, default);
        Assert.True(result.IsSuccess);
    }
    [Fact]
    public async Task Should_Fail_If_Movie_Already_Exists()
    {
        var request = new CreateMovieCommand(
            Title: "A New Hope",
            EpisodeId: 4,
            OpenningCrawl: "It is a period of civil war. Rebel spaceships, striking from a hidden base, have won their first victory against the evil Galactic Empire.",
            Director: "George Lucas",
            Producer: "Gary Kurtz"
        );
        _movieRepositoryMock.GetAllMoviesAsync().Returns(new List<Movie>(){
            new Movie
            {
                Id = 1,
                Episode = 4,
                Title = "A New Hope",
                OpenningCrawl = "It is a period of civil war. Rebel spaceships, striking from a hidden base, have won their first victory against the evil Galactic Empire.",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Director = "George Lucas",
                Producer = "Gary Kurtz"
            }
        });

        var result = await _handler.Handle(request, default);

        Assert.True(result.IsFailure);
    }
}
