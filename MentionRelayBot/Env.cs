namespace MentionRelayBot;

public class Env
{
    public static string GetOrThrow(string key)
    {
        var value = Environment.GetEnvironmentVariable(key);
        if (value == null)
        {
            throw new ArgumentException($"No environment variable found with key {key}");
        }

        return value;
    }
}
