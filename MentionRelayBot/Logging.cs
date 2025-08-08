using Discord;

namespace MentionRelayBot;

public static class Logging
{
    public static Task Log(LogMessage arg)
    {
        var prefix = arg.Severity switch
        {
            LogSeverity.Critical => "CRIT",
            LogSeverity.Error => "ERRO",
            LogSeverity.Warning => "WARN",
            LogSeverity.Info => "INFO",
            LogSeverity.Verbose => "VERB",
            LogSeverity.Debug => "DEBG",
            _ => throw new ArgumentOutOfRangeException()
        };

        Console.WriteLine($"[{prefix}]: {arg.Message}");
        if (arg.Exception != null)
            Console.WriteLine(arg.Exception);

        return Task.CompletedTask;
    }

    public static void Warn(string msg)
    {
        Console.WriteLine($"[WARN]: {msg}");
    }

    public static void Error(string msg)
    {
        Console.WriteLine($"[ERRO]: {msg}");
    }
}
