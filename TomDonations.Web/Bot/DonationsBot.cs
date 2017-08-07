using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace TomDonations.Web {
    public class DonationsBot {
        private readonly IServiceProvider _service;
        private DiscordSocketClient _client;
        private CommandService _commands;

        public DonationsBot(IServiceProvider service) {
            _service = service;
        }

        public async Task MainAsync() {
            _commands = new CommandService();
            _client = new DiscordSocketClient();
            _client.Log += Log;

            string token = "MzQzNDcwNjg4MzUyNjAwMDY1.DGepsg.GWlk1KIMMW3TJc29DQ1X54cGpGw";

            await InstallCommands();
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        //private IServiceProvider InitializeServices() {
        //    var collection = new ServiceCollection();

        //    collection.AddSingleton(DatabaseService.Instance);

        //    return collection.BuildServiceProvider();
        //}

        private async Task InstallCommands() {
            _client.MessageReceived += HandleCommand;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        private async Task HandleCommand(SocketMessage messageParam) {
            var message = messageParam as SocketUserMessage;
            if (message == null) return;

            int argPos = 0;

            if (!(message.HasCharPrefix('!', ref argPos) ||
                  message.HasMentionPrefix(_client.CurrentUser, ref argPos))) return;

            var context = new CommandContext(_client, message);

            var result = await _commands.ExecuteAsync(context, argPos, _service);

            if (!result.IsSuccess) {
                await context.Channel.SendMessageAsync(result.ErrorReason);
            }
        }

        //private async Task MessageReceived(SocketMessage msg) {
        //    if (msg.Content == "!ping") {
        //        await msg.Channel.SendMessageAsync("Pong!");
        //    }
        //}

        private Task Log(LogMessage msg) {
            Console.WriteLine(msg);
            return Task.CompletedTask;
        }
    }
}