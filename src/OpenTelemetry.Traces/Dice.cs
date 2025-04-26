namespace OpenTelemetry.Traces;

public class Dice
{
    public List<int> Roll(int rolls)
    {
        var results = new List<int>();

        for (var i = 0; i < rolls; i++)
        {
            results.Add(Random.Shared.Next(1, 6));
        }

        return results;
    }
}