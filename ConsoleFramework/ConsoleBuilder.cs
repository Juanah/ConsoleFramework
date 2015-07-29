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
    /// Creates Instances of ConsoleManager
    /// Jonas Ahlf 29.07.2015 21:43:52
    /// </summary>
    public class ConsoleBuilder
    {
        private readonly TextReader _reader;
        private readonly TextWriter _writer;

        /// <summary>
        /// New instance of ConsoleBuilder,
        /// nessary is textreader/writer
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        public ConsoleBuilder(TextReader reader, TextWriter writer)
        {
            _reader = reader;
            _writer = writer;
        }

        /// <summary>
        /// Creates a new manager with given reader/writer and Commands
        /// </summary>
        /// <param name="commands">Commands Which can be loaded with Options</param>
        /// <returns></returns>
        public IConsoleManager Build(IList<Command> commands)
        {
            IConsoleManager manager = new ConsoleManager(commands,_reader,_writer);
            return manager;
        }

    }
}
