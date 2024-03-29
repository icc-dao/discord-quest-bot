using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using IResult = Discord.Commands.IResult;

namespace ICCD.QuestBot.Services;

/// <summary>
/// Handles discord commands.
/// </summary>
public class CommandHandlingService
{
    private readonly CommandService _commands;
    private readonly DiscordSocketClient _discord;
    private readonly IServiceProvider _services;

    /// <summary>
    /// Initializes a new instance of the <see cref="CommandHandlingService"/> class.
    /// </summary>
    /// <param name="services">The DI service provider.</param>
    public CommandHandlingService(IServiceProvider services)
    {
        _commands = services.GetRequiredService<CommandService>();
        _discord = services.GetRequiredService<DiscordSocketClient>();
        _services = services;

        // Hook CommandExecuted to handle post-command-execution logic.
        _commands.CommandExecuted += CommandExecutedAsync;

        // Hook MessageReceived so we can process each message to see if it qualifies as a command.
        _discord.MessageReceived += MessageReceivedAsync;
    }

    /// <summary>
    /// Initializes the service.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task InitializeAsync()
    {
        // Register modules that are public and inherit ModuleBase<T>.
        await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
    }

    public async Task MessageReceivedAsync(SocketMessage rawMessage)
    {
        // Ignore system messages, or messages from other bots
        if (!(rawMessage is SocketUserMessage message))
        {
            return;
        }

        if (message.Source != MessageSource.User)
        {
            return;
        }

        // This value holds the offset where the prefix ends
        var argPos = 0;
        if (!message.HasCharPrefix('!', ref argPos))
        {
            return;
        }

        var context = new SocketCommandContext(_discord, message);
        // Perform the execution of the command. In this method,
        // the command service will perform precondition and parsing check
        // then execute the command if one is matched.
        await _commands.ExecuteAsync(context, argPos, _services);
        // Note that normally a result will be returned by this format, but here
        // we will handle the result in CommandExecutedAsync,
    }

    public async Task CommandExecutedAsync(Optional<CommandInfo> command, ICommandContext context, IResult result)
    {
        // command is unspecified when there was a search failure (command not found); we don't care about these errors
        if (!command.IsSpecified)
        {
            return;
        }

        // the command was successful, we don't care about this result, unless we want to log that a command succeeded.
        if (result.IsSuccess)
        {
            return;
        }

        // the command failed, let's notify the user that something happened.
        await context.Channel.SendMessageAsync($"error: {result}");
    }
}