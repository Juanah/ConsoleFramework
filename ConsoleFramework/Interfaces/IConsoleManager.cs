using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleFramework.Interfaces
{
    public interface IConsoleManager
    {
        /// <summary>
        /// Starts the framework
        /// </summary>
        /// <param name="threaded"></param>
        void Run(bool threaded);
        bool RegisterCommandEvent(Command command,EventHandler commandTask);
        bool UnRegisterCommandEvent(Command command);
        //EventHandler RegisterCommandEvent(Command command);
    }
}
