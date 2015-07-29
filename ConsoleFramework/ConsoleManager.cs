using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public ConsoleManager(IList<Command> allCommands, TextReader consoleReader, TextWriter consoleWriter)
        {
            _allCommands = allCommands;
            _consoleReader = consoleReader;
            _consoleWriter = consoleWriter;
        }

        public void Run()
        {
            throw new NotImplementedException();
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

            }
        }
    }
}
