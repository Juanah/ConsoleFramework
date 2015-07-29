using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleFramework.Data
{
    /// <summary>
    /// Represents one Option which can have various 
    /// Jonas Ahlf 29.07.2015 10:33:08
    /// </summary>
    public class Option
    {
        /// <summary>
        /// String as an Identifier which have to be typed in
        /// </summary>
        public String Identifier { get; set; }

        /// <summary>
        /// Declares if this Options can have parameters
        /// </summary>
        public bool HaveParameters { get; set; }



        /// <summary>
        /// List of parameters
        /// </summary>
        public IList<Parameter> Parameters { get; set; }

    }
}
