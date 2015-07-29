using System;
using System.Linq;
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
        private static Command _secondCommand;
        private static Option _firstOption;
        private static EventHandler _handler;
        private static EventHandler _secondEventHandler;
        static void Main(string[] args)
        {
            _consoleBuilder = new ConsoleBuilder(Console.In,Console.Out);
            CreateManager();
            _handler = OnFirstCommand;
            _secondEventHandler = SecondEventHandler;

            if (!_consoleManager.RegisterCommandEvent(_secondCommand,_secondEventHandler) ||
                !_consoleManager.RegisterCommandEvent(_firstCommand, _handler))
            {
                Console.WriteLine("Could not register command delegate");
                Console.ReadKey();
            }
            else
            {
                _consoleManager.Run(false);
            }
        }

        private static void SecondEventHandler(object sender, EventArgs eventArgs)
        {
            CommandEventArgs args = (CommandEventArgs)eventArgs;
            Console.WriteLine("Command {0} triggered", args.Command.Name);
            if (args.Command.HasOptions)
            {
                var firstOrDefault = args.Command.Options.FirstOrDefault();
                if (firstOrDefault != null)
                    Console.WriteLine("Options {0} triggered",firstOrDefault.Identifier);
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
            _firstOption = new Option {HaveParameters = false, Identifier = "f", Parameters = null};
            _secondCommand = new Command("copy",CaseLevel.Non,new[]{_firstOption});
            _consoleManager = _consoleBuilder.Build(new[] { _firstCommand,_secondCommand });
        }

    }
}
