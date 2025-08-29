using System.Linq.Expressions;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using RestaurantBookingAPI.Data;

namespace RestauantBookingAPI.Middleware
{
    
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        

        public GlobalExceptionMiddleware(RequestDelegate next,ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }

        }
        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            
            var (statusCode, message) = exception switch
            {   ArgumentNullException => (HttpStatusCode.BadRequest, "Required data is missing"),
                ArgumentException => (HttpStatusCode.BadRequest, exception.Message),
                InvalidOperationException => (HttpStatusCode.Conflict, exception.Message),
                KeyNotFoundException => (HttpStatusCode.NotFound, "Resource not found"),
                UnauthorizedAccessException => (HttpStatusCode.Unauthorized, "Unauthorized access"),
                NotImplementedException => (HttpStatusCode.NotImplemented, "Feature not implemented"),
                TimeoutException => (HttpStatusCode.RequestTimeout, "Request timeout"),
                _ => (HttpStatusCode.InternalServerError, "An internal server error occurred")
            };
            context.Response.StatusCode = (int)statusCode;
            var response = new
            {
           
                    message = message,
                    statusCode = (int)statusCode,
                    timestamp = DateTime.UtcNow
                
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }));
        }
        
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class GlobalExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionMiddleware>();
        }
    }
}
