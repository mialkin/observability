using OpenTelemetry.Resources;

namespace GrafanaLabs.Api.Configurations.OpenTelemetry;

public static class OpenTelemetryConfiguration
{
    public static void ConfigureOpenTelemetry(this IHostApplicationBuilder applicationBuilder)
    {
        var settings = applicationBuilder.Configuration.GetSettings<OpenTelemetrySettings>();

        applicationBuilder.Services
            .AddOpenTelemetry()
            .ConfigureResource(builder => builder
                .AddService(settings.ServiceName, serviceVersion: settings.ServiceVersion))
            .WithMetrics(builder => builder
                .AddMeter(settings.ServiceName));
        // .WithTracing(builder => builder
        //     .AddSource(settings.ServiceName)
        //     .AddAspNetCoreInstrumentation()
        //     .AddOtlpExporter(x => x.Endpoint = new Uri(settings.JaegerUrl)));

        // applicationBuilder.Logging
        //     .AddOpenTelemetry(options =>
        //     {
        //         var builder = ResourceBuilder
        //             .CreateDefault()
        //             .AddService(settings.ServiceName, serviceVersion: settings.ServiceVersion);
        //
        //         options.SetResourceBuilder(builder);
        //     });

        applicationBuilder.Services
            .AddSingleton<Instrumentation>(_ =>
                new Instrumentation(
                    activitySourceName: settings.ServiceName,
                    activitySourceVersion: settings.ServiceVersion));
    }
}
