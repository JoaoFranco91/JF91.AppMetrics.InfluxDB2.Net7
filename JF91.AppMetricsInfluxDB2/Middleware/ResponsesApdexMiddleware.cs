﻿namespace JF91.AppMetricsInfluxDB2.Middleware;

using App.Metrics;
using App.Metrics.Apdex;
using Microsoft.AspNetCore.Http;

public class ResponsesApdexMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IMetrics _metrics;

    public ResponsesApdexMiddleware
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
            
            var apdex = new ApdexOptions
            {
                Name = "apdex",
                Context = "myApi",
                ApdexTSeconds = 0.5, // Adjust based on your SLA
                Tags = tags
            };
            
            using(_metrics.Measure.Apdex.Track(apdex))
            {
                await _next(context);
            }
        }
        catch (Exception ex)
        {
        }
    }
}