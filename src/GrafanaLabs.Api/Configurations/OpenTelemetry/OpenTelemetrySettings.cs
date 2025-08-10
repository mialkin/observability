namespace GrafanaLabs.Api.Configurations.OpenTelemetry;

public class OpenTelemetrySettings
{
    public required string ServiceName { get; init; }
    public required string ServiceVersion { get; init; }
}
