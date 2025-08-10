namespace OpenTelemetry.Traces;

public class Dice(Configurations.OpenTelemetry.Instrumentation instrumentation)
{
    public List<int> Roll(int rolls)
    {
        var results = new List<int>();

        using var activity = instrumentation.ActivitySource.StartActivity("dice.roll_the_dice");

        for (var i = 0; i < rolls; i++)
        {
            using var childActivity = instrumentation.ActivitySource.StartActivity("dice.roll_once");

            results.Add(Random.Shared.Next(1, 6));
        }

        return results;
    }
    // https://opentelemetry.io/docs/languages/dotnet/instrumentation/#traces
    // https://stackoverflow.com/questions/51541323/recommended-ram-ratios-for-elk-with-docker-compose
    // https://hub.docker.com/_/elasticsearch
    // https://www.jaegertracing.io/docs/2.5/elasticsearch/
}
