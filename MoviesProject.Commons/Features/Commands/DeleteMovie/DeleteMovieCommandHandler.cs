using MoviesProject.Commons.Abstractions;
using MoviesProject.Commons.Database.Repositories.Interfaces;
using MoviesProject.Commons.Shared;

namespace MoviesProject.Commons.Features.Commands.DeleteMovie;

public sealed class DeleteMovieCommandHandler(
    IMovieRepository movieRepository
) : ICommandHandler<DeleteMovieCommand, DeleteMovieCommandResponse>
{
    private readonly IMovieRepository _MovieRepository = movieRepository;
    public async Task<Result<DeleteMovieCommandResponse>> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
    {
        var movieToDelete = await _MovieRepository.GetMovieByIdAsync(request.Id);
        if (movieToDelete is null)
        {
            return Result<DeleteMovieCommandResponse>.Failure("Movie not found");
        }
        await _MovieRepository.DeleteMovieAsync(movieToDelete);

        return Result<DeleteMovieCommandResponse>.Success(new DeleteMovieCommandResponse(movieToDelete.Id));
    }
}
