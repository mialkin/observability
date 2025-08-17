using System.Diagnostics.Metrics;

namespace GrafanaLabs.Api.Telemetry.Meters;

public class SimpleMeter(Meter meter)
{
    private readonly Counter<long> _counter = meter.CreateCounter<long>("simple_meter_total");
    public void Count(int value) => _counter.Add(value);
}
