using MediatR;
using MoviesProject.Commons.Shared;

namespace MoviesProject.Commons.Abstractions;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{

}
