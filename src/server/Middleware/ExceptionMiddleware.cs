
using FluentValidation;
using System.Net;
using System.Text.Json;

namespace server.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
            catch ( ValidationException ex )
            {
                _logger.LogWarning("Validation failed: {Errors}", ex.Errors);
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";

                var errors = ex.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray());

                var response = new { type = "ValidationError", errors };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            catch ( KeyNotFoundException ex )
            {
                _logger.LogWarning("Not found: {Message}", ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Response.ContentType = "application/json";
                var response = new { type = "NotFound", message = ex.Message };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            catch ( Exception ex )
            {
                _logger.LogError(ex, "Unhandled exception");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var response = new { type = "ServerError", message = "Внутрішня помилка сервера" };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }

    }
}
