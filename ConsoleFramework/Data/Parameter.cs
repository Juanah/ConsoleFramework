using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleFramework.Data
{
    /// <summary>
    /// Represents a Value parameter example: "run 'testfile.txt'"
    /// Jonas Ahlf 29.07.2015 10:16:32
    /// </summary>
    public class Parameter
    {
        /// <summary>
        /// Rawvalue as string 
        /// </summary>
        public string RawValue { get; set; }

        /// <summary>
        /// Tries to convert String value to integer
        /// </summary>
        /// <returns></returns>
        public virtual int? GetIntValue()
        {
            int intVal;
            if (!int.TryParse(RawValue, out intVal))
            {
                return null;
            }
            return intVal;
        }

        /// <summary>
        /// Tries to convert string value to double
        /// </summary>
        /// <returns></returns>
        public virtual double? GetDoubleValue()
        {
            double doubleVal;
            if (!double.TryParse(RawValue,out doubleVal))
            {
                return null;
            }
            return doubleVal;
        }

    }
}
