using Microsoft.Extensions.Configuration;

namespace Application;

public static class ConfigurationExtensions
{
    public static string[] GetArray(this IConfigurationSection configurationSection)
    {
        var children = configurationSection.GetChildren().Select(c => c.Value).ToList();
        var values = new List<string>();
        foreach (var entry in children)
        {
            if (entry is not null)
            {
                values.Add(entry);
            }
        }

        return values.ToArray();
    }
}