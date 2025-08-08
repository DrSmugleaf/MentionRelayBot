# MentionRelayBot
Relays mentions from anywhere in a specific server to a specific channel.

Useful for when you are not getting mentions consistently in a Discord server.

## Environment variables
- DISCORD_TOKEN: The bot's token, created in https://discord.com/developers/applications
- DISCORD_GUILD: The server's ID number.
- DISCORD_USER: The user's ID to listen for mentions to. Everyone pings, direct pings, and role pings for roles that they have will be relayed.
- DISCORD_DESTINATION_CHANNEL: The channel's ID to relay mentions into.
