using MoviesProject.Commons.Database.Repositories.Interfaces;
using MoviesProject.Commons.Features.Commands.DeleteMovie;
using MoviesProject.Commons.Models;
using NSubstitute;

namespace MoviesProject.Tests.Handlers;

public class DeleteMovieHandlerTests
{
    private readonly IMovieRepository _movieRepositoryMock = Substitute.For<IMovieRepository>();
    private readonly DeleteMovieCommandHandler _handler;

    public DeleteMovieHandlerTests()
    {
        _handler = new DeleteMovieCommandHandler(_movieRepositoryMock);
    }

    [Fact]
    public async Task Should_Delete_Movies_Ok()
    {
        _movieRepositoryMock.GetMovieByIdAsync(Arg.Any<int>()).Returns(new Movie
        {
            Id = 1,

        });

        _movieRepositoryMock.DeleteMovieAsync(Arg.Any<Movie>()).Returns(Task.CompletedTask);

        var request = new DeleteMovieCommand(1);
        var result = await _handler.Handle(request, default);
        Assert.True(result.IsSuccess);
        await _movieRepositoryMock.Received(1).DeleteMovieAsync(Arg.Is<Movie>(m => m.Id == 1));
    }

    [Fact]
    public async Task Should_Fail_When_Movie_Does_Not_Exists()
    {

        _movieRepositoryMock.GetMovieByIdAsync(Arg.Any<int>()).Returns((Movie?)null);
        _movieRepositoryMock.DeleteMovieAsync(Arg.Any<Movie>()).Returns(Task.CompletedTask);

        var request = new DeleteMovieCommand(1);
        var result = await _handler.Handle(request, default);
        Assert.True(result.IsFailure);
    }
}
