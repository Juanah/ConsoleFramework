using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConsoleFramework.Data;
using ConsoleFramework.Interfaces;

namespace ConsoleFramework
{
    /// <summary>
    /// Controlclass which handels the User commands
    /// It will be created by the ConsoleBuilder
    /// Jonas Ahlf 29.07.2015 11:18:50
    /// </summary>
    internal class ConsoleManager: IConsoleManager
    {
        private IList<Command> _allCommands;
        private Dictionary<Command, EventHandler> _eventList = new Dictionary<Command, EventHandler>();
        private TextReader _consoleReader;
        private TextWriter _consoleWriter;
        private CommandParser _commandParser;
        public ConsoleManager(IList<Command> allCommands, TextReader consoleReader, TextWriter consoleWriter)
        {
            _allCommands = allCommands;
            _consoleReader = consoleReader;
            _consoleWriter = consoleWriter;
            _commandParser = new CommandParser(_allCommands.ToList());
        }

        public void Run(bool threaded)
        {
            if (threaded)
            {
                var threadStart = ThreadPool.QueueUserWorkItem(ConsoleParsingTaks);
                _consoleWriter.WriteLine(
                    threadStart ? "Console Framework started {0}" : "Console Framework failed to start {0}", DateTime.Now);
            }
            else
            {
                _consoleWriter.WriteLine("Console Framework mainThread started {0}",DateTime.Now);
                ConsoleParsingTaks(new object());
            }
            
        }

        public bool RegisterCommandEvent(Command command,EventHandler commandTask)
        {
            if (_eventList.ContainsKey(command))
            {
                return false;
            }
            _eventList.Add(command,commandTask);
            return true;
        }

        public bool UnRegisterCommandEvent(Command command)
        {
            throw new NotImplementedException();
        }

        void ConsoleParsingTaks(object state)
        {
            while (true)
            {
                var consoleInput = _consoleReader.ReadLine();
                var commands = _commandParser.ParseCommands(consoleInput);
                foreach (var command in commands)
                {
                    var tempEventHandler = getEventHandlerFromCommand(command);
                    if (tempEventHandler == null)
                    {
                        continue;
                    }
                    tempEventHandler(this, new CommandEventArgs{Command = command});
                }
            }
        }

        EventHandler getEventHandlerFromCommand(Command command)
        {

            var keys = _eventList.Keys;
            var key = keys.FirstOrDefault(d => d.Name.Equals(command.Name));

            if (key == null || !_eventList.ContainsKey(key))
            {
                return null;
            }

            return _eventList[key];
        }
    }
}
