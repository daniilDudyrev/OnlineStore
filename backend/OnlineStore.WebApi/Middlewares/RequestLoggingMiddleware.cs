namespace OnlineStore.WebApi.Middlewares;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Response.HasStarted)
        {
            var requestPath = context.Request.Path;
            await context.Response.WriteAsync(requestPath);
            // _logger.LogInformation(requestPath);
            await _next(context);
        }
    }
}