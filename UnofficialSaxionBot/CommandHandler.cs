﻿using System.Reflection;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

public class CommandHandler
{
    private CommandService service;
    private DiscordSocketClient client;

    public CommandHandler(DiscordSocketClient client)
    {
        this.client = client;
        service = new CommandService();
    }

    public async Task InitAsync()
    {
        client.MessageReceived += HandleCommandAsync;
        await service.AddModulesAsync(Assembly.GetEntryAssembly(), null);
    }

    private async Task HandleCommandAsync(SocketMessage socketMessage)
    {
        // Don't process the command if it was a system message
        var message = socketMessage as SocketUserMessage;
        if (message == null) return;

        // Create a number to track where the prefix ends and the command begins
        int argPos = 0;

        // Determine if the message is a command based on the prefix and make sure no bots trigger commands
        if (!(message.HasCharPrefix('!', ref argPos) ||
            message.HasMentionPrefix(client.CurrentUser, ref argPos)) ||
            message.Author.IsBot)
            return;

        // Create a WebSocket-based command context based on the message
        var context = new SocketCommandContext(client, message);

        // Execute the command with the command context we just
        // created, along with the service provider for precondition checks.
        await service.ExecuteAsync(context, argPos, null);
    }
}
