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
        private static Command _parameterCommand;
        private static Option _firstOption;
        private static Option _parameterOption;
        private static EventHandler _handler;
        private static EventHandler _secondEventHandler;
        private static EventHandler _parameterEventHandler;
        static void Main(string[] args)
        {
            _consoleBuilder = new ConsoleBuilder(Console.In,Console.Out);
            CreateManager();
            _handler = OnFirstCommand;
            _secondEventHandler = SecondEventHandler;
            _parameterEventHandler = OnParameterCommandTriggered;
            if (!_consoleManager.RegisterCommandEvent(_secondCommand,_secondEventHandler) ||
                !_consoleManager.RegisterCommandEvent(_firstCommand, _handler) || 
                !_consoleManager.RegisterCommandEvent(_parameterCommand, _parameterEventHandler))
            {
                Console.WriteLine("Could not register command delegate");
                Console.ReadKey();
            }
            else
            {
                _consoleManager.Run(true);
                _consoleManager.RawInput("hallo");
                while (true)
                {
                    Console.ReadKey();
                }
            }
        }

        private static void OnParameterCommandTriggered(object sender, EventArgs eventArgs)
        {
            CommandEventArgs args = (CommandEventArgs)eventArgs;
            Console.WriteLine("Command {0} triggered", args.Command.Name);
            if (args.Command.HasOptions)
            {
                var option = args.Command.Options.FirstOrDefault();
                if (option == null) return;
                Console.WriteLine("Options {0} triggered", option.Identifier);
                if (option.HaveParameters)
                {
                    var parameter = option.Parameters.FirstOrDefault();
                    if (parameter == null)
                    {
                        return;
                    }
                    Console.WriteLine("Parameter with value {0}",parameter.RawValue);
                }
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
            _firstCommand = new Command("hallo");
            _firstOption = new Option {HaveParameters = false, Identifier = "f", Parameters = null};
            _parameterOption = new Option {HaveParameters = true,Identifier = "p"};
            _secondCommand = new Command("copy",new[]{_firstOption},false);
            _parameterCommand = new Command("pm",new []{_parameterOption},true);
            _consoleManager = _consoleBuilder.Build(new[] { _firstCommand,_secondCommand ,_parameterCommand});

        }

    }
}
