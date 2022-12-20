using System.Collections.Concurrent;

namespace OnlineStore.WebApi.Middlewares;

public class PageTransitionsMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ConcurrentDictionary<string, int> _transitions = new();

    public PageTransitionsMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var requestPath = context.Request.Path.ToString();
        if (requestPath == "/metrics")
        {
            await context.Response.WriteAsJsonAsync(_transitions);
        }
        else
        {
            _transitions.AddOrUpdate(context.Request.Path,
                _ => 1,
                (_, currentCount) => currentCount + 1
            );
            await _next(context);
        }
    }
}