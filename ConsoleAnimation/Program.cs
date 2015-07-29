using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleFramework;
using ConsoleFramework.Data;
using ConsoleFramework.Interfaces;

namespace ConsoleAnimation
{
    class Program
    {
        private static ConsoleBuilder _consoleBuilder;
        private static IConsoleManager _consoleManager;
        private static Command _firstCommand;
        private static EventHandler _handler;
        static void Main(string[] args)
        {
            _consoleBuilder = new ConsoleBuilder(Console.In,Console.Out);
            CreateManager();
            _handler = OnFirstCommand;
            bool registerCommandEvent = _consoleManager.RegisterCommandEvent(_firstCommand, _handler);
            if (!registerCommandEvent)
            {
                Console.WriteLine("Could not register command delegate");
                Console.ReadKey();
            }
            else
            {
                _consoleManager.Run(false);
            }
        }

        private static void OnFirstCommand(object sender, EventArgs eventArgs)
        {
            CommandEventArgs args = (CommandEventArgs) eventArgs;
            Console.WriteLine("Command {0} triggered", args.Command.Name);
        }

        static void CreateManager()
        {
            _firstCommand = new Command("hallo", CaseLevel.Low);
            _consoleManager = _consoleBuilder.Build(new[] { _firstCommand });
        }

    }
}
