using System.Diagnostics;

namespace OpenTelemetry.Traces.Configurations.OpenTelemetry;

public class Instrumentation(string activitySourceName, string activitySourceVersion) : IDisposable
{
    public ActivitySource ActivitySource { get; } = new(activitySourceName, activitySourceVersion);

    public void Dispose()
    {
        ActivitySource.Dispose();
    }
}