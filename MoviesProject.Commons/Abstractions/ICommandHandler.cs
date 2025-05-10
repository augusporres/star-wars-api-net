using MediatR;
using MoviesProject.Commons.Shared;

namespace MoviesProject.Commons.Abstractions;

public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>
{

}
