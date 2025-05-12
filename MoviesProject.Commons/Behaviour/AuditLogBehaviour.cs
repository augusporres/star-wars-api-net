using System.Reflection;
using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Logging;
using MoviesProject.Commons.Attributes;

namespace MoviesProject.Commons.Behaviour;

public class AuditLogBehaviour<TRequest, TResponse>(
    ILogger<AuditLogBehaviour<TRequest, TResponse>> logger
) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<AuditLogBehaviour<TRequest, TResponse>> _Logger = logger;
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var auditLogAttributes = request.GetType().GetCustomAttributes<AuditLogAttribute>();
        if (auditLogAttributes.Any())
        {
            _Logger.LogInformation($"IN -> with request {request.GetType().Name} -> {JsonSerializer.Serialize(request)}");
        }
        var result = await next();

        if (auditLogAttributes.Any())
        {
            _Logger.LogInformation($"OUT -> with request {request.GetType().Name} -> {JsonSerializer.Serialize(result)}");
        }

        return result;
    }
}
