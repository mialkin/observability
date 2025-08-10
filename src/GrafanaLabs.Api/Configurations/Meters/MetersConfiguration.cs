using System.Diagnostics.Metrics;

namespace GrafanaLabs.Api.Configurations.Meters;

public static class MetersConfiguration
{
    public static void ConfigureMeters(this IServiceCollection services)
    {
        services.AddSingleton<Meter>(_ => new Meter("default meter"));
        services.AddSingleton<SimpleMeter>();
    }
}
