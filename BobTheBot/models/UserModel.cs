using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace BobTheBot.database {
    public class UserModel {
        public ulong id;
        public DateTimeOffset lastChatActivity;
        public Dictionary<string, object> values = new Dictionary<string, object>();
    }
}
