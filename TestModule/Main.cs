using System;
using BobTheBot;

namespace TestModule {
    public class Main {
        public Main() {
            Console.WriteLine($"TestModule :: Loaded - Testing settings : {BotMain.instance.settings.Options.Token}");
        }
    }
}
