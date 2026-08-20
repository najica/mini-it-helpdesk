using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace MiniItHelpdesk.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Neuhvaćena greška na {Method} {Path}",
                    context.Request.Method, context.Request.Path);

                if (context.Response.HasStarted)
                    throw;

                context.Response.Clear();
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var problem = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Došlo je do neočekivane greške.",
                    Detail = "Pokušajte ponovo kasnije ili kontaktirajte podršku.",
                    Instance = context.Request.Path
                };
                problem.Extensions["traceId"] = context.TraceIdentifier;

                await context.Response.WriteAsJsonAsync(
                    problem,
                    options: null,
                    contentType: MediaTypeNames.Application.ProblemJson);
            }
        }
    }
}