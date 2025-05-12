using FluentValidation;
using MediatR;

namespace MoviesProject.Commons.Behaviour;

public class ValidationBehaviour<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators
) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _Validators = validators;
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_Validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(_Validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults.Where(r => r.Errors.Any()).SelectMany(r => r.Errors).ToList();

            if (failures.Count != 0)
            {
                throw new ValidationException(failures);
            }
        }
        return await next();
    }
}
