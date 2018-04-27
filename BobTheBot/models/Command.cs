using System;
using System.Collections.Generic;
using System.Text;
using Discord.WebSocket;

namespace BobTheBot.models {
    public class Command {
        // Last argument is ALWAYS greedy (greedy meaning it wants all the remaining arguments)

        public string commandName;
        public List<Argument> arguments = new List<Argument>();
        public Action<List<Argument>, SocketMessage> callBack;

        public void AddToRegistry() {
            if (commandName == "" || callBack == null)
                throw new Exception("Command :: AddToRegistry() invalid, not all required fields are set!");

            CommandRegistry.instance.RegisterCommand(this);
        }
    }

    public class Argument {
        public string name;
        public string summary = "";
        public string data = "";

        public Argument(string name) {
            this.name = name;
        }
    }
}
