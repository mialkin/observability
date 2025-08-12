using System.Diagnostics.Metrics;

namespace GrafanaLabs.Api.Telemetry.Meters;

public static class MetersConfiguration
{
    public static void ConfigureMeters(this IServiceCollection services)
    {
        services.AddSingleton<Meter>(_ => new Meter(OtelScopeName.Default));
        services.AddSingleton<SimpleMeter>();
    }
}
