using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using MoviesProject.Commons.Database.Repositories.Interfaces;
using MoviesProject.Commons.Features.Queries.GetAllMovies;
using MoviesProject.Commons.Models;
using NSubstitute;
using Xunit;

namespace MoviesProject.Tests.Handlers;

public class GetAllMoviesHandlerTests
{
    private readonly IMovieRepository _movieRepositoryMock = Substitute.For<IMovieRepository>();
    private readonly GetAllMoviesQueryHandler _handler;

    public GetAllMoviesHandlerTests()
    {
        _handler = new GetAllMoviesQueryHandler(_movieRepositoryMock);
    }

    [Fact]
    public async Task Should_Return_Movies_Ok()
    {
        var moviesInDb = new Faker<Movie>()
            .RuleFor(m => m.Id, f => f.Random.Int())
            .RuleFor(m => m.Episode, f => f.Random.Int())
            .RuleFor(m => m.Title, f => f.Lorem.Sentence())
            .RuleFor(m => m.CreatedAt, f => f.Date.Past())
            .RuleFor(m => m.UpdatedAt, f => f.Date.Past())
            .RuleFor(m => m.Director, f => f.Person.FullName)
            .RuleFor(m => m.Producer, f => f.Person.FullName)
            .Generate(5);
        _movieRepositoryMock.GetAllMoviesAsync().Returns(moviesInDb);
        var result = await _handler.Handle(new GetAllMoviesQuery(), default);


        Assert.True(result.IsSuccess);
        Assert.Equal(5, result?.Value?.Movies.Count);
    }

    [Fact]
    public async Task Should_Return_Movies_Empty()
    {
        var moviesInDb = new List<Movie>();
        _movieRepositoryMock.GetAllMoviesAsync().Returns(moviesInDb);
        var result = await _handler.Handle(new GetAllMoviesQuery(), default);

        Assert.True(result.IsSuccess);
        Assert.Empty(result?.Value?.Movies);
    }
}
