using MediatR;
using MoviesProject.Commons.Shared;

namespace MoviesProject.Commons.Abstractions;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{

}
