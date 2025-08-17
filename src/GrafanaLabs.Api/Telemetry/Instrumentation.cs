using System.Diagnostics;

namespace GrafanaLabs.Api.Telemetry;

public class Instrumentation(string activitySourceName, string activitySourceVersion) : IDisposable
{
    public ActivitySource ActivitySource { get; } = new(activitySourceName, activitySourceVersion);

    public void Dispose()
    {
        ActivitySource.Dispose();
    }
}
