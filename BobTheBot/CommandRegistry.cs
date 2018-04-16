using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace BobTheBot {
    class CommandRegistry {

        private readonly Dictionary<string, List<Action<string[]>>> _commandRegistry = new Dictionary<string, List<Action<string[]>>>();

        public CommandRegistry() {
            BotMain.client.MessageReceived += ClientOnMessageReceived;
        }

        private async Task ClientOnMessageReceived(SocketMessage socketMessage) {
            // First we decide if its actually a command or just a normal message
        }

        public void RegisterCommand(string cmd, Action<string[]> callback) {
            if (!_commandRegistry.ContainsKey(cmd)) {
                _commandRegistry.Add(cmd, new List<Action<string[]>>());
            }

            _commandRegistry[cmd].Add(callback);
        }

        public void DeregisterCommand(string cmd, Action<string[]> callback)
        {
            if (!_commandRegistry.ContainsKey(cmd)) return;

            _commandRegistry[cmd].Remove(callback);
        }

    }
}
