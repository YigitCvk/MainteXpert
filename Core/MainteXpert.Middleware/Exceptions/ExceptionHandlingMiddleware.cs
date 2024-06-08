namespace MainteXpert.Middleware.Exceptions
{
    public sealed class ExceptionHandlingMiddleware : IMiddleware
    {
        // private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        //private readonly RedisCacheService _redisCacheService;
        private readonly IConfiguration _configuration;
        public ExceptionHandlingMiddleware(
            //    ILogger<ExceptionHandlingMiddleware> logger,
            //  RedisCacheService redisCacheService,
            IConfiguration configuration)
        {
            // _logger = logger;
            //   _redisCacheService = redisCacheService;
            _configuration = configuration;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);

                /*
                if (GetLicenceValidation())
                    await next(context);
                else
                    await LicenceExceptionAsync(context);
                */
            }
            catch (Exception e)
            {
                StringBuilder sb = new StringBuilder(e.Message);
                if (e.InnerException != null)
                    sb.Append(" [InnerExp: " + e.InnerException.Message + "]");
                if (e is ValidationException validationException)
                    sb.Append(" [ValErrors:" + System.Text.Json.JsonSerializer.Serialize(validationException.ErrorsDictionary.ToList()) + "]");
                // _logger.LogError(e, sb.ToString());
                await HandleExceptionAsync(context, e);
            }
        }
        private bool GetLicenceValidation()
        {
            /*
           var cacheModel = _redisCacheService.Get<CacheModel>(CacheKey.LicenceValidatorKey.ToString());
            var connectionString = _configuration.GetConnectionString("SqlServerConnection");
            var cacheTimeIntervalInSec = _configuration.GetSection("CacheConnectionStrings:RedisCahceTimeIntervalInSec").Value;
            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    var cmd = new SqlCommand("SELECT GETDATE()", conn);
                    conn.Open();
                    var dt = (DateTime)cmd.ExecuteScalar();
                    var dtTicks = dt.Ticks;
                    var tickDiff = dtTicks - cacheModel.CacheTimeTicks;
                    var tickDiffTotalSec = TimeSpan.FromTicks(tickDiff).TotalSeconds;
                    if (tickDiffTotalSec > Convert.ToDouble(cacheTimeIntervalInSec))
                    {
                        _logger.LogError($"{String.Format("{0:dd.MM.yyyy HH:mm:ss}", DateTime.Now)} - Lisans kontrol tarihi güncel değil!. Son kontrol tarih:{TimeSpan.FromTicks(cacheModel.CacheTimeTicks)}");
                        return false;
                    }
                    else
                        return (bool)cacheModel.Value;
                }
                catch (Exception ex)
                {
                    _logger.LogError($"{String.Format("{0:dd.MM.yyyy HH:mm:ss}", DateTime.Now)} - Lisans kontrol, SQL Server hatası." + ex.Message);
                    return false;
                }
            }
            */
            return true;
        }
        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var statusCode = GetStatusCode(exception);
            var response = new
            {
                title = GetTitle(exception),
                innerexp = exception.InnerException != null ? exception.InnerException.Message : null,
                errors = GetErrors(exception),
            };

            var responseModel = new
            {
                data = response,
                IsSuccess = false,
                Message = $"İşlem Başarısız {exception.Message}",
                HttpStatus = statusCode

            };

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(responseModel));
        }
        private static async Task LicenceExceptionAsync(HttpContext httpContext)
        {
            var statusCode = StatusCodes.Status426UpgradeRequired;
            var response = new
            {
                title = "Licence Error",
                status = statusCode,
                detail = "Licence Invalid"
            };
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
        }
        private static int GetStatusCode(Exception exception) =>
            exception switch
            {
                //BadRequestException => StatusCodes.Status400BadRequest,
                //  NotFoundException => StatusCodes.Status404NotFound,
                ValidationException => StatusCodes.Status422UnprocessableEntity,
                _ => StatusCodes.Status500InternalServerError
            };
        private static string GetTitle(Exception exception) =>
            exception switch
            {
                ApplicationException applicationException => applicationException.Title,
                _ => "Server Error"
            };
        private static IReadOnlyDictionary<string, string[]> GetErrors(Exception exception)
        {
            IReadOnlyDictionary<string, string[]> errors = null;
            if (exception is ValidationException validationException)
            {
                errors = validationException.ErrorsDictionary;
            }
            return errors;
        }
    }
}