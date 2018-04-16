using System;
using System.Threading.Tasks;
using BobTheBot;

namespace ConsoleFrame {
    class Program {
        public static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync() {

            Console.WriteLine("ConsoleFrame :: Initialized.. Initializing bot main");
            BotMain bob = new BotMain();;

            await Task.Delay(-1);
        }
    }
}
