using Discord;
using Discord.WebSocket;
using System;
using System.Linq;
using System.Threading.Tasks;
using Discord.Rest;

// Use the following link to invite bot to servers https://discordapp.com/oauth2/authorize?client_id=415283644114927647&scope=bot&permissions=2146958591

#pragma warning disable 1587
/// <summary author="Timo Heijne">
/// The brains of the actual bot. Here we connect and setup most of the base functionality. We would reference this in a console application (in this bots case ConsoleFrame)
/// </summary>
#pragma warning restore 1587
namespace BobTheBot {
    public class BotMain {
        public static BotMain instance;
        public static DiscordSocketClient client;

        public Settings settings { get; private set; }
        public UserData userData { get; private set; }

        private ModuleLoader _moduleLoader;

        public BotMain() {
            MainAsync();
        }

        public async Task MainAsync() {
            instance = this;
            Console.WriteLine("BotMain :: Initializing Bot");

            // Initialize required Dependencies
            client = new DiscordSocketClient();
            settings = new Settings();
            userData = new UserData();
            _moduleLoader = new ModuleLoader();

            if (!settings.IsValid) {
                Console.WriteLine("Looks like its the first startup! settings file has been generated. Please set the token and other settings there!");
                return;
            }

            // Initialize basic delegates 
            client.MessageReceived += MessageReceived;
            client.Ready += ClientOnReady;

            Console.WriteLine("BotMain :: Connecting to discord");

            await client.LoginAsync(TokenType.Bot, settings.Options.Token);
            await client.StartAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private async Task ClientOnReady() {
            await client.SetGameAsync(settings.Options.PlayingStatus);

            Console.WriteLine("BotMain :: Connection to discord.. Successfull");
        }

        private async Task MessageReceived(SocketMessage message) {
            Console.WriteLine($"MessageReceived :: {message.Author.Id}({message.Author.Username}) > {message.Content} | Total Num Messages: 0");

            int totalMessages = userData.GetValue<int>(message.Author.Id, "NumMessages", 0);
            totalMessages += 1;
            userData.SetValue(message.Author.Id, "NumMessages", totalMessages);

            string[] content = message.Content.Split(' ');

            var rMessage = (RestUserMessage)await message.Channel.GetMessageAsync(message.Id);
            SocketGuildChannel guild = message.Channel as SocketGuildChannel;

            IEmote emote = guild.Guild.Emotes.First(e => e.Name == "silvanhoofd");
            await rMessage.AddReactionAsync(emote);

            if (content[0] == ".ping") {
                await message.Channel.SendMessageAsync("Pong!");
            }
        }
    }
}