using System;
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
        private CaseLevel _caseLevel;
        private IList<Option> _options;
        private IList<Parameter> _parameters;
        private bool _hasOptions, _hasParamertes;
        public string Name { get { return _name; } set { _name = value; } }
        public CaseLevel CaseLevel { get { return _caseLevel; } }
        public IList<Option> Options { get { return _options; } set { _options = value; }}
        public IList<Parameter> Parameters { get { return _parameters; } }

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

        public Command(string name, CaseLevel caseLevel)
        {
            _name = name;
            _caseLevel = caseLevel;
            _hasOptions = false;
            _hasParamertes = false;
        }

        public Command(string name, CaseLevel caseLevel, IList<Option> options, IList<Parameter> parameters)
        {
            _name = name;
            _caseLevel = caseLevel;
            _options = options;
            _parameters = parameters;
            _hasOptions = true;
            _hasParamertes = true;
        }

        public Command(string name, CaseLevel caseLevel, IList<Option> options)
        {
            _name = name;
            _caseLevel = caseLevel;
            _options = options;
            _hasOptions = true;
        }

        public Command(string name, CaseLevel caseLevel, IList<Parameter> parameters)
        {
            _name = name;
            _caseLevel = caseLevel;
            _parameters = parameters;
            _hasParamertes = true;
        }
        #endregion
    }
}
