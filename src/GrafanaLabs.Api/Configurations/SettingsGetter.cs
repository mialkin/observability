using Ardalis.GuardClauses;

namespace GrafanaLabs.Api.Configurations;

public static class SettingsGetter
{
    public static T GetSettings<T>(this IConfiguration configuration)
    {
        var settings = configuration.GetRequiredSection(key: typeof(T).Name).Get<T>();
        return Guard.Against.Null(settings);
    }
}
