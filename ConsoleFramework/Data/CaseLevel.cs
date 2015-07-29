using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleFramework.Data
{
    /// <summary>
    /// Hierarchy of the caselevels,
    /// CaseLevel means how exact the option or command 
    /// must be typed in
    /// Jonas Ahlf 29.07.2015 10:38:33
    /// </summary>
    public enum CaseLevel
    {
        /// <summary>
        /// Strict
        /// </summary>
        Non,
        /// <summary>
        /// not casesenitiv
        /// </summary>
        Low,
        /// <summary>
        /// not casesenitiv, ignores invalid parameters
        /// </summary>
        Medium,
    }
}
