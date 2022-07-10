// <copyright file="Program.cs" company="ICCD">
// Copyright (c) ICCD. All rights reserved.
// </copyright>

using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using ICCD.QuestBot.Data.Quests;
using ICCD.QuestBot.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ICCD.QuestBot
{
    /// <summary>
    /// Main program entry.
    /// </summary>
    public class Program
    {
        private readonly IConfiguration _configuration;
        private ServiceProvider _services;

        /// <summary>
        /// Initializes a new instance of the <see cref="Program"/> class.
        /// </summary>
        private Program()
        {
            var environmentName = System.Environment.GetEnvironmentVariable("ENVIRONMENT_NAME");
            _configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables(prefix: "ICCD_")
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile($"appsettings.{environmentName}.json", true)
                .Build();
        }

        // There is no need to implement IDisposable like before as we are
        // using dependency injection, which handles calling Dispose for us.
        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            var botToken = _configuration["token"];
            if (string.IsNullOrEmpty(botToken))
            {
                throw new Exception("TOKEN is not specified, please specify a bot token by either " +
                                    "modifying appsettings.json or specify the ICCD_TOKEN environment variable.");
            }
            // You should dispose a service provider created using ASP.NET
            // when you are finished using it, at the end of your app's lifetime.
            // If you use another dependency injection framework, you should inspect
            // its docvarumentation for the best way to do this.
            await using (_services = ConfigureServices())
            {
                var client = _services.GetRequiredService<DiscordSocketClient>();

                client.Log += LogAsync;
                _services.GetRequiredService<CommandService>().Log += LogAsync;

                // Tokens should be considered secret data and never hard-coded.
                // We can read from the environment variable to avoid hard coding.
                await client.LoginAsync(TokenType.Bot, _configuration["token"]);
                await client.StartAsync();


                client.UserJoined += ClientOnUserJoined;
                client.ReactionAdded += ClientOnReactionAdded;
                client.ReactionRemoved += ClientOnReactionRemoved;

                // Here we initialize the logic required to register our commands.
                await _services.GetRequiredService<CommandHandlingService>().InitializeAsync();

                await Task.Delay(Timeout.Infinite);
            }
        }

        private async Task ClientOnReactionRemoved(Cacheable<IUserMessage, ulong> arg1,
            Cacheable<IMessageChannel, ulong> arg2, SocketReaction arg3)
        {
            ProcessReaction(arg3.User.Value, arg3.MessageId, arg3.Emote, QuestManager.ReactionAction.Remove);
        }

        private async Task ClientOnReactionAdded(Cacheable<IUserMessage, ulong> arg1,
            Cacheable<IMessageChannel, ulong> arg2, SocketReaction arg3)
        {
            ProcessReaction(arg3.User.Value, arg3.MessageId, arg3.Emote, QuestManager.ReactionAction.Add);
        }

        private void ProcessReaction(IUser user, ulong messageId, IEmote emote,
            QuestManager.ReactionAction reactionAction)
        {
            var client = _services.GetRequiredService<DiscordSocketClient>();
            if (client.CurrentUser.Id.Equals(user.Id))
            {
                return;
            }

            var questManager = _services.GetRequiredService<QuestManager>();
            questManager.ProcessReaction(user, messageId, emote, reactionAction);
        }

        private async Task ClientOnUserJoined(SocketGuildUser arg)
        {
            var questManager = _services.GetRequiredService<QuestManager>();
            questManager.ProcessOnJoinQuests(arg);
        }

        private Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log.ToString());

            return Task.CompletedTask;
        }

        private ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton(_ =>
                {
                    var cfg = new DiscordSocketConfig
                    {
                        GatewayIntents = GatewayIntents.All,
                        AlwaysDownloadUsers = true,
                    };

                    return new DiscordSocketClient(cfg);
                })
                .AddSingleton<CommandService>()
                .AddSingleton<CommandHandlingService>()
                .AddSingleton<HttpClient>()
                .AddSingleton<QuestConfigurationManager>(QuestConfigurationManager.Instance)
                .AddSingleton<QuestManager>()
                .BuildServiceProvider();
        }
    }
}