﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleFramework.Data;

namespace ConsoleFramework
{

    /// <summary>
    /// Represents one Command which can have verious paramaeters and options
    /// Jonas Ahlf 29.07.2015 10:14:01
    /// </summary>
    public class Command
    {
        private string _name;
        private IList<Option> _options;
        private bool _hasOptions, _hasParamertes;
        public string Name { get { return _name; } set { _name = value; } }
        public IList<Option> Options { get { return _options; } set { _options = value; }}

        public bool HasOptions
        {
            get { return _hasOptions; }
            set { _hasOptions = value; }
        }

        public bool HasParameters { get { return _hasParamertes; } }

        #region Constructor

        public Command()
        {
        }

        public Command(string name)
        {
            _name = name;
            _hasOptions = false;
            _hasParamertes = false;
        }

        public Command(string name, IList<Option> options,bool hasParameters)
        {
            _name = name;
            _options = options;
            _hasOptions = true;
            _hasParamertes = hasParameters;
        }
        #endregion
    }
}
