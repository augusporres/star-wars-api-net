namespace MoviesProject.Commons.Features.Queries.GetMovieDetails;
public sealed record GetMovieDetailsByIdQueryResponse(
    string Title,
    int EpisodeId,
    string OpenningCrawl
);
