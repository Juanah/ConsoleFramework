using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleFramework.Data
{
    /// <summary>
    /// Arguments which will be passed when Command triggered
    /// Jonas Ahlf 29.07.2015 11:34:31
    /// </summary>
    public class CommandEventArgs : EventArgs
    {
        /// <summary>
        /// Just the Command which triggered, with all Data inside
        /// such as Options and Parameters
        /// </summary>
        public Command Command { get; set; }
    }
}
