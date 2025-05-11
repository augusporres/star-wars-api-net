using MoviesProject.Commons.Abstractions;
using MoviesProject.Commons.Database.Repositories.Interfaces;
using MoviesProject.Commons.Shared;

namespace MoviesProject.Commons.Features.Queries.GetMovieDetails;

public sealed class GetMovieDetailsByIdQueryHandler : IQueryHandler<GetMovieDetailsByIdQuery, GetMovieDetailsByIdQueryResponse>
{
    private readonly IMovieRepository _movieRepository;

    public GetMovieDetailsByIdQueryHandler(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<Result<GetMovieDetailsByIdQueryResponse>> Handle(GetMovieDetailsByIdQuery request, CancellationToken cancellationToken)
    {
        var movie = await _movieRepository.GetMovieByIdAsync(request.Id);
        if (movie is null)
        {
            return Result<GetMovieDetailsByIdQueryResponse>.Failure($"Movie with id {request.Id} not found.");
        }
        return Result<GetMovieDetailsByIdQueryResponse>.Success(new GetMovieDetailsByIdQueryResponse(movie.Title, movie.Episode, movie.OpenningCrawl));
    }
}
