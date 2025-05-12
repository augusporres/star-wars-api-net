using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoviesProject.Commons.Database.Repositories.Interfaces;
using MoviesProject.Commons.Features.Queries.GetMovieDetails;
using MoviesProject.Commons.Models;
using NSubstitute;

namespace MoviesProject.Tests.Handlers;

public class GetMovieDetailsHandlerTests
{
    private readonly IMovieRepository _movieRepositoryMock = Substitute.For<IMovieRepository>();
    private readonly GetMovieDetailsByIdQueryHandler _handler;
    public GetMovieDetailsHandlerTests()
    {
        _handler = new GetMovieDetailsByIdQueryHandler(_movieRepositoryMock);
    }

    [Fact]
    public async Task Should_Return_Movie_Ok()
    {
        var movieInDb = new Movie
        {
            Id = 1,
            Episode = 4,
            Title = "A New Hope",
            OpenningCrawl = "It is a period of civil war. Rebel spaceships, striking from a hidden base, have won their first victory against the evil Galactic Empire.",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Director = "George Lucas",
            Producer = "Gary Kurtz"
        };
        _movieRepositoryMock.GetMovieByIdAsync(Arg.Any<int>()).Returns(movieInDb);
        var result = await _handler.Handle(new GetMovieDetailsByIdQuery(1), default);

        Assert.True(result.IsSuccess);
        Assert.Equal("A New Hope", result?.Value?.Title);
    }

    [Fact]
    public async Task Should_Return_Error_When_MovieId_Is_Not_Found()
    {

        _movieRepositoryMock.GetMovieByIdAsync(Arg.Any<int>()).Returns((Movie?)null);
        var result = await _handler.Handle(new GetMovieDetailsByIdQuery(1), default);

        Assert.True(result.IsFailure);
        Assert.Null(result?.Value);
    }
}
