namespace JF91.AppMetricsInfluxDB2.Middleware;

using System.Net;
using App.Metrics;
using App.Metrics.Counter;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

public class HttpStatusCodesCounterMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IMetrics _metrics;

    public HttpStatusCodesCounterMiddleware
    (
        RequestDelegate next,
        IHostEnvironment env,
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
                    "user",
                    "status",
                    "status_code",
                },
                new[]
                {
                    context.Request.Method,
                    context.Request.Path.Value,
                    "myself",
                    Enum.GetName(typeof(HttpStatusCode), context.Response.StatusCode),
                    context.Response.StatusCode.ToString()
                }
            );
            
            _metrics.Measure.Counter.Increment
            (
                new CounterOptions
                {
                    Name = "http_status_codes",
                    Context = "myApi",
                    MeasurementUnit = Unit.Requests,
                    Tags = tags
                }
            );
        }
        catch (Exception ex)
        {
            var tags = new MetricTags
            (
                new[]
                {
                    "method",
                    "path",
                    "user",
                    "status",
                    "status_code",
                },
                new[]
                {
                    context.Request.Method,
                    context.Request.Path.Value,
                    "myself",
                    HttpStatusCode.InternalServerError.ToString(),
                    ((int)HttpStatusCode.InternalServerError).ToString()
                }
            );
            
            _metrics.Measure.Counter.Increment
            (
                new CounterOptions
                {
                    Name = "http_status_codes",
                    Context = "myApi",
                    MeasurementUnit = Unit.Errors,
                    Tags = tags
                }
            );
        }
    }
}