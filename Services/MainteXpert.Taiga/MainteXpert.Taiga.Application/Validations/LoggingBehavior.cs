namespace MainteXpert.Taiga.Application.Validations
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public LoggingBehavior(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var stopwatch = Stopwatch.StartNew();
            var requestName = request.GetType().Name;
            var requestGuid = Guid.NewGuid().ToString();
            var requestNameWithGuid = $"{requestName} [{requestGuid}]";
            _logger.LogInformation($"[START] {requestNameWithGuid}");
            TResponse response;
            JsonSerializerSettings settings = new JsonSerializerSettings();
            // settings.ContractResolver = new IncludeJsonContentAttributesResolver();
            // settings.Formatting = Formatting.Indented;
            try
            {
                try
                {
                    _logger.LogInformation($"[PROPS] {requestNameWithGuid} {JsonConvert.SerializeObject(request)}");
                }
                catch (NotSupportedException)
                {
                    _logger.LogWarning($"[Serialization ERROR] {requestNameWithGuid} Could not serialize the request.");
                }
                response = await next();
                try
                {
                    _logger.LogInformation($"[RESPONSE] {requestNameWithGuid} {JsonConvert.SerializeObject(response)}");
                }
                catch (Exception)
                {
                    _logger.LogWarning($"[Serialization ERROR] {requestNameWithGuid} Could not serialize the response.");
                }
            }
            finally
            {
                stopwatch.Stop();
                _logger.LogInformation(
                    $"[END] {requestNameWithGuid}; Execution time={stopwatch.ElapsedMilliseconds}ms");
            }
            return response;
        }
    }
}
