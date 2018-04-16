using System;
using System.Collections.Generic;
using System.Text;

namespace BobTheBot.models {
    public class SettingsModel {
        public bool Debug = false;
        public string Token;
        public string PlayingStatus;
        public string[] Modules = new string[0];
        public string cmdPrefix = ".";
    }
}
