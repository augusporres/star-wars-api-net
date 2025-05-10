using MediatR;
using MoviesProject.Commons.Shared;

namespace MoviesProject.Commons.Abstractions;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{

}
