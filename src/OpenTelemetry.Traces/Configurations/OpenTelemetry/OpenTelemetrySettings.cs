namespace OpenTelemetry.Traces.Configurations.OpenTelemetry;

public record OpenTelemetrySettings(string ServiceName, string ServiceVersion, string JaegerUrl);