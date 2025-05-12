using MoviesProject.Commons.Abstractions;
using MoviesProject.Commons.Database.Repositories.Interfaces;
using MoviesProject.Commons.Shared;

namespace MoviesProject.Commons.Features.Commands.UpdateMovie;

public sealed class UpdateMovieCommandHandler(
    IMovieRepository movieRepository
) : ICommandHandler<UpdateMovieCommand, UpdateMovieCommandResponse>
{
    private readonly IMovieRepository _MovieRepository = movieRepository;
    public async Task<Result<UpdateMovieCommandResponse>> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await _MovieRepository.GetMovieByIdAsync(request.Id);
        if (movie is null)
        {
            return Result<UpdateMovieCommandResponse>.Failure("Movie not found");
        }

        movie.Title = request.Title;
        movie.OpenningCrawl = request.OpenningCrawl;
        movie.Director = request.Director;
        movie.Producer = request.Producer;
        movie.Episode = request.EpisodeId;

        await _MovieRepository.UpdateMovieAsync(movie);
        return Result<UpdateMovieCommandResponse>.Success(new UpdateMovieCommandResponse(movie.Id));
    }
}
