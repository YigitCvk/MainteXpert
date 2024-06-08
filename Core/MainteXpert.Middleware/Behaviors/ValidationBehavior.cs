namespace MainteXpert.Middleware.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : class
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
        /*
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                return await next();
            }
            var context = new ValidationContext<TRequest>(request);
            var errorMessages = _validators
                .Select(x => x.Validate(context))
                .SelectMany(x => x.Errors)
                .Where(x => x != null)
                .Select(x => x.ErrorMessage)
                .ToArray();
            //.GroupBy(
            //    x => x.PropertyName,
            //    x => x.ErrorMessage,
            //    (propertyName, errorMessages) => new
            //    {
            //        Key = propertyName,
            //        Values = errorMessages.Distinct().ToArray()
            //    })
            //.ToDictionary(x => x.Key, x => x.Values);
            if (errorMessages.Any())
            {
                var dictionary = new Dictionary<string, string[]>() { { "Messages", errorMessages } };
                throw new Middlewares.Exceptions.ValidationException(dictionary);
            }
            return await next();
        }
        */
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (!_validators.Any())
            {
                return await next();
            }
            var context = new ValidationContext<TRequest>(request);
            var errorMessages = _validators
                .Select(x => x.Validate(context))
                .SelectMany(x => x.Errors)
                .Where(x => x != null)
                .Select(x => x.ErrorMessage)
                .ToArray();
            //.GroupBy(
            //    x => x.PropertyName,
            //    x => x.ErrorMessage,
            //    (propertyName, errorMessages) => new
            //    {
            //        Key = propertyName,
            //        Values = errorMessages.Distinct().ToArray()
            //    })
            //.ToDictionary(x => x.Key, x => x.Values);
            if (errorMessages.Any())
            {
                var dictionary = new Dictionary<string, string[]>() { { "Messages", errorMessages } };


                throw new MainteXpert.Middleware.Exceptions.ValidationException(dictionary);
            }
            return await next();
        }

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }




}