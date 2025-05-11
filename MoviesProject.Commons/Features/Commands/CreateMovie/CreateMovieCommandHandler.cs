using Microsoft.Extensions.Logging;
using MoviesProject.Commons.Abstractions;
using MoviesProject.Commons.Database.Repositories.Interfaces;
using MoviesProject.Commons.Models;
using MoviesProject.Commons.Shared;

namespace MoviesProject.Commons.Features.Commands.CreateMovie;

public sealed class CreateMovieCommandHandler(
    IMovieRepository movieRepository,
    ILogger<CreateMovieCommandHandler> logger
) : ICommandHandler<CreateMovieCommand, CreateMovieCommandResponse>
{
    private readonly IMovieRepository _movieRepository = movieRepository;

    public readonly ILogger<CreateMovieCommandHandler> _Logger = logger;

    public async Task<Result<CreateMovieCommandResponse>> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var movie = new Movie
            {
                Title = request.Title,
                Episode = request.EpisodeId,
                Director = request.Director,
                Producer = request.Producer,
                OpenningCrawl = request.OpenningCrawl
            };
            int id = await _movieRepository.AddMovieAsync(movie);
            return Result<CreateMovieCommandResponse>.Success(new CreateMovieCommandResponse(id));
        }
        catch (Exception ex)
        {
            _Logger.LogError($"Exception found during movie creation {ex}");
            return Result<CreateMovieCommandResponse>.Failure("Error ocurred found during movie creation");
        }
    }
}
