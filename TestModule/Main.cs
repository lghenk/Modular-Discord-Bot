using System;
using System.Collections.Generic;
using BobTheBot;
using BobTheBot.models;
using Discord;
using Discord.WebSocket;

namespace TestModule {
    public class Main {
        public Main() {
            // Lets register a command;
            Command newCmd = new Command();
            newCmd.commandName = "test";
            newCmd.arguments.Add(new Argument("Arg 1"));
            newCmd.arguments.Add(new Argument("Arg 2"));
            newCmd.arguments.Add(new Argument("Arg 3"));
            newCmd.arguments.Add(new Argument("Arg Greedy"));
            newCmd.callBack += CallBackTest;
            newCmd.AddToRegistry();

            Command pingCmd = new Command();
            pingCmd.commandName = "ping";
            pingCmd.callBack += CallBackPing;
            pingCmd.AddToRegistry();
        }

        private void CallBackPing(List<Argument> arguments, SocketMessage socketMessage)
        {
            socketMessage.Channel.SendMessageAsync("Pong!");
        }

        private void CallBackTest(List<Argument> arguments, SocketMessage socketMessage)
        {
            EmbedBuilder eb =new EmbedBuilder();
            eb.WithTitle("Received Test Command");
            eb.WithAuthor(socketMessage.Author);
            eb.WithColor(Color.DarkBlue);

            foreach (var arg in arguments)
            {
                EmbedFieldBuilder efb = new EmbedFieldBuilder();
                efb.WithName(arg.name);
                efb.WithValue(arg.data);
                efb.WithIsInline(true);
                eb.AddField(efb);
            }

            socketMessage.Channel.SendMessageAsync("", false, eb.Build());
        }
    }
}
