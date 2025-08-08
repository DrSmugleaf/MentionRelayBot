using Discord;
using Discord.WebSocket;

namespace MentionRelayBot;

public sealed class Bot
{
    private DiscordSocketClient _client;
    private ulong _guildId;
    private ulong _userId;
    private ulong _discordDestinationChannel;

    public Bot(DiscordSocketClient client)
    {
        _client = client;
        _guildId =  ulong.Parse(Env.GetOrThrow("DISCORD_GUILD"));
        _userId = ulong.Parse(Env.GetOrThrow("DISCORD_USER"));
        _discordDestinationChannel = ulong.Parse(Env.GetOrThrow("DISCORD_DESTINATION_CHANNEL"));
        _client.MessageReceived += OnMessageReceived;
    }

    async Task OnMessageReceived(SocketMessage arg)
    {
        if (arg.Author.IsBot ||
            arg.Channel is not IGuildChannel guildChannel ||
            guildChannel.GuildId != _guildId)
        {
            return;
        }

        var user = _client.GetGuild(_guildId).GetUser(_userId);
        if (!arg.MentionedEveryone &&
            !arg.MentionedUserIds.Contains(_userId) &&
            !arg.MentionedRoleIds.Any(id => user.Roles.Any(r => r.Id == id)))
        {
            return;
        }

        var channel = await _client.GetChannelAsync(_discordDestinationChannel);
        if (channel is not ITextChannel textChannel)
        {
            Logging.Error($"Channel {_discordDestinationChannel} is not a {nameof(ITextChannel)}");
            return;
        }

        await textChannel.SendMessageAsync($"<@{_userId}> {arg.GetJumpUrl()}\n{arg.Author.Username}:\n```\n{arg.CleanContent}\n```");
    }
}
