using System.Text.Json;
using Vendra.Core.Common;

namespace Vendra.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger,
            IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;

                var errors = _env.IsDevelopment()
                    ? new List<string>
                    {
                        ex.ToString(),
                        ex.InnerException?.ToString() ?? string.Empty
                    }
                    : null;

                var response = new APIResponse<object>(
                    statusCode: 500,
                    isSuccess: false,
                    errorMessage: _env.IsDevelopment()
                        ? ex.Message
                        : "Internal Server Error Occurred",
                    errors: errors
                );

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}