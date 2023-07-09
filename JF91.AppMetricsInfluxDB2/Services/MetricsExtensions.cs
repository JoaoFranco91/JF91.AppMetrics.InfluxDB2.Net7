namespace JF91.AppMetricsInfluxDB2.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

public static class MetricsExtensions
{
    public static IServiceCollection AddMetricsServices
    (
        this IServiceCollection services
    )
    {
        services.AddAppMetricsCollectors()
            .AddMvc()
            .AddMetrics();

        return services;
    }
}