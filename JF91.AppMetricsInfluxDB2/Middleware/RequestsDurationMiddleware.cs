namespace JF91.AppMetricsInfluxDB2.Middleware;

using App.Metrics;
using App.Metrics.Timer;
using Microsoft.AspNetCore.Http;

public class RequestsDurationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IMetrics _metrics;

    public RequestsDurationMiddleware
    (
        RequestDelegate next,
        IMetrics metrics
    )
    {
        _next = next;
        _metrics = metrics;
    }

    public async Task InvokeAsync
    (
        HttpContext context
    )
    {
        try
        {
            var tags = new MetricTags
            (
                new[]
                {
                    "method",
                    "path",
                    "user"
                },
                new[]
                {
                    context.Request.Method,
                    context.Request.Path.Value,
                    "myself"
                }
            );
            
            var requestTimer = new TimerOptions
            {
                Name = "http_requests_duration",
                Context = "myApi",
                MeasurementUnit = Unit.Requests,
                DurationUnit = TimeUnit.Milliseconds,
                RateUnit = TimeUnit.Milliseconds,
                Tags = tags
            };
            
            using(_metrics.Measure.Timer.Time(requestTimer))
            {
                await _next(context);
            }
        }
        catch (Exception ex)
        {
        }
    }
}