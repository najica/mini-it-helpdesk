using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MiniItHelpdesk.Middleware
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            if (httpContext.Response.HasStarted)
            {
                _logger.LogWarning("Odgovor je već počeo da se šalje. Izuzetak se ne može obraditi.");
                return false;
            }

            _logger.LogError(exception, "Unhandled exception occurred at {Method}/{Path}",
                httpContext.Request.Method,
                httpContext.Request.Path);

            var (statusCode, title) = exception switch
            {
                InvalidOperationException => (StatusCodes.Status409Conflict, "Konflikt sa trenutnim stanjem resursa."),

                ArgumentException => (StatusCodes.Status400BadRequest, "Neispravan unos."),

                KeyNotFoundException => (StatusCodes.Status404NotFound, "Traženi resurs nije pronađen."),

                _ => (StatusCodes.Status500InternalServerError, "Došlo je do neočekivane greške na serveru.")
            };

            httpContext.Response.StatusCode = statusCode;

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = exception.Message 
            };

            problemDetails.Extensions["traceId"] = httpContext.TraceIdentifier;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}