using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoviesProject.Commons.Database.Repositories.Interfaces;
using MoviesProject.Commons.Features.Commands.UpdateMovie;
using MoviesProject.Commons.Models;
using NSubstitute;

namespace MoviesProject.Tests.Handlers;

public class UpdateMovieHandlerTests
{
    private readonly IMovieRepository _movieRepositoryMock = Substitute.For<IMovieRepository>();
    private readonly UpdateMovieCommandHandler _handler;
    public UpdateMovieHandlerTests()
    {
        _handler = new UpdateMovieCommandHandler(_movieRepositoryMock);
    }

    [Fact]
    public async Task Should_Update_Movie_Ok()
    {
        var movieBefore = new Movie
        {
            Id = 1,
            Title = "Star Wars: Episode IV - A New Hope",
            OpenningCrawl = "It is a period of civil war. Rebel spaceships, striking from a hidden base, have won their first victory against the evil Galactic Empire.",
            Director = "George Lucas",
            Producer = "Gary Kurtz",
            Episode = 4
        };
        _movieRepositoryMock.GetMovieByIdAsync(Arg.Any<int>())
            .Returns(movieBefore);

        var command = new UpdateMovieCommand(
            Id: 1,
            Title: "Star Wars: Episode V - The Empire Strikes Back",
            OpenningCrawl: "It is a dark time for the Rebellion. Although the Death Star has been destroyed, Imperial troops have driven the Rebel forces from their hidden base and pursued them across the galaxy.",
            Director: "Irvin Kershner",
            Producer: "Gary Kurtz",
            EpisodeId: 4
        );
        _movieRepositoryMock.UpdateMovieAsync(Arg.Any<Movie>())
            .Returns(Task.FromResult(1));

        var result = await _handler.Handle(command, CancellationToken.None);
        Assert.True(result.IsSuccess);
        Assert.Equal(1, result?.Value?.Id);

    }
    [Fact]
    public async Task Should_Fail_When_Movie_Does_not_Exists()
    {

        _movieRepositoryMock.GetMovieByIdAsync(Arg.Any<int>())
            .Returns((Movie?)null);

        var command = new UpdateMovieCommand(
            Id: 1,
            Title: "Star Wars: Episode V - The Empire Strikes Back",
            OpenningCrawl: "It is a dark time for the Rebellion. Although the Death Star has been destroyed, Imperial troops have driven the Rebel forces from their hidden base and pursued them across the galaxy.",
            Director: "Irvin Kershner",
            Producer: "Gary Kurtz",
            EpisodeId: 4
        );
        _movieRepositoryMock.UpdateMovieAsync(Arg.Any<Movie>())
            .Returns(Task.FromResult(1));

        var result = await _handler.Handle(command, CancellationToken.None);
        Assert.True(result.IsFailure);
        Assert.Equal("Movie not found", result.Error);

    }
}
