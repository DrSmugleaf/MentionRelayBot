using Discord;
using Discord.WebSocket;
using MentionRelayBot;

if (File.Exists("./.env"))
{
    foreach (var env in await File.ReadAllLinesAsync("./.env"))
    {
        var split = env.Split("=");
        if (split.Length != 2)
        {
            Logging.Warn($"Error reading .env variable, expected 2 values after splitting, got {split.Length}");
            continue;
        }

        Environment.SetEnvironmentVariable(split[0], split[1]);
    }
}

var client = new DiscordSocketClient(new DiscordSocketConfig
{
    GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent
});
client.Log += Logging.Log;

var token = Env.GetOrThrow("DISCORD_TOKEN");
await client.LoginAsync(TokenType.Bot, token);
await client.StartAsync();

var bot = new Bot(client);
await Task.Delay(-1);
