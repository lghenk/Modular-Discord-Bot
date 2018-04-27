using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BobTheBot.models;
using Discord;
using Discord.WebSocket;


namespace BobTheBot {
    class CommandRegistry {
        public static CommandRegistry instance;
        private readonly Dictionary<string, Command> _commandRegistry = new Dictionary<string, Command>();

        public CommandRegistry() {
            instance = this;
            BotMain.client.MessageReceived += ClientOnMessageReceived;
        }

        private async Task ClientOnMessageReceived(SocketMessage socketMessage) {
            // First we decide if its actually a command or just a normal message
            if (!IsValidPrefix(socketMessage.Content)) return;

            string[] raw = socketMessage.Content.Split(' ');
            raw[0] = raw[0].Replace(BotMain.instance.settings.Options.cmdPrefix, "");
            if (_commandRegistry.ContainsKey(raw[0])) {
                Console.WriteLine($"CommandRegistry :: Command {raw[0]} has been found... Executing blah blah blah");
                Command cmd = _commandRegistry[raw[0]];
                List<Argument> arguments = cmd.arguments;
                foreach (var args in arguments) {
                    args.data = "";
                }

                if (raw.Length > arguments.Count) {
                    // We received enough arguments
                    for (int i = 0; i < arguments.Count; i++) {

                        if (i == arguments.Count - 1) {
                            // This is the last argument get everything except previous arguements
                            string d = "";
                            for (int j = i + 1; j < raw.Length; j++) {
                                d += raw[j] + ((j == raw.Length - 1) ? "" : " ");
                            }
                            arguments[i].data = d;
                        } else {
                            arguments[i].data = raw[i + 1];
                        }
                    }

                    cmd.callBack?.Invoke(arguments, socketMessage);
                } else {
                    Console.WriteLine("Invalid Arg Count");
                    string args = "";
                    foreach (var argument in arguments)
                    {
                        args += $"`[{argument.name}]` ";
                    }
                    
                    await socketMessage.Channel.SendMessageAsync($"Command invalid expected {arguments.Count} arguments: {args}");
                }
            }
        }

        private bool IsValidPrefix(string message) {
            string cmdPrefix = BotMain.instance.settings.Options.cmdPrefix;
            if (message.Length < cmdPrefix.Length) return false;

            for (int i = 0; i < cmdPrefix.Length; i++) {
                if (cmdPrefix[i] != message[i])
                    return false;
            }

            return true;
        }

        public void RegisterCommand(Command cmd) {
            if (!_commandRegistry.ContainsKey(cmd.commandName)) {
                Console.WriteLine($"CommandRegistry :: Adding command {cmd.commandName}");
                _commandRegistry.Add(cmd.commandName, cmd);
            } else {
                throw new Exception("CommandRegistry :: Command already exists!");
            }
        }

        public void DeregisterCommand(string cmd) {
            if (!_commandRegistry.ContainsKey(cmd)) return;

            _commandRegistry.Remove(cmd);
        }

    }
}
