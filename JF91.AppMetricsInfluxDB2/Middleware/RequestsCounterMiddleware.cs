using System.Net;
using System.Text.Json;

namespace JF91.AppMetricsInfluxDB2.Middleware;

using App.Metrics;
using App.Metrics.Counter;
using Microsoft.AspNetCore.Http;

public class RequestsCounterMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IMetrics _metrics;

    public RequestsCounterMiddleware
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
            await _next(context);

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

            _metrics.Measure.Counter.Increment
            (
                new CounterOptions
                {
                    Name = "http_requests_count",
                    Context = "myApi",
                    MeasurementUnit = Unit.Calls,
                    Tags = tags
                }
            );
        }
        catch (Exception ex)
        {
        }
    }
}