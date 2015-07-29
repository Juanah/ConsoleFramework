using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ConsoleFramework.Data;

namespace ConsoleFramework
{
    /// <summary>
    /// Converts Raw userinput to Commands
    /// Jonas Ahlf 29.07.2015 20:08:53
    /// </summary>
    public class CommandParser
    {
        private readonly List<Command> _allCommands;

        public CommandParser(List<Command> allCommands)
        {
            _allCommands = allCommands;
            this.Initialize();
        }

        #region Initialization

        private void Initialize()
        {
            CreateCommandMappings();
        }

        private void CreateCommandMappings() //Creates Automapper mappings for cloning Commands
        {
            Mapper.CreateMap<Command, Command>()
                .ForMember(s => s.Options, d => d.Ignore())
                .ForMember(s => s.Parameters, d => d.Ignore());
            Mapper.CreateMap<Option, Option>()
                .ForMember(s => s.Parameters, d => d.Ignore());
        }
        #endregion



        #region Member

        public IList<Command> ParseCommands(string rawInput)
        {
            if (String.IsNullOrWhiteSpace(rawInput))
            {
                return null;
            }
            var parsedCommands = new List<Command>();

            var commandNames = GetCommandNames(rawInput);

            foreach (var commandName in commandNames)
            {
                var validateCommand = ValidateCommand(commandName);
                if (validateCommand == null)
                {
                    break;
                }

                if (validateCommand.FoundCommand.HasOptions)
                {
                    var options = GetOptions(commandName, validateCommand.FoundCommand);
                    if (options != null)
                    {
                        validateCommand.CloneCommand.Options = options;
                    }
                    else
                    {
                        validateCommand.CloneCommand.HasOptions = false;
                    }
                }
                parsedCommands.Add(validateCommand.CloneCommand);
            }

            return parsedCommands;
        }

        public IList<CommandName> GetCommandNames(string rawInput)
        {
            IList<CommandName> parsedCommands = new List<CommandName>();

            if (String.IsNullOrWhiteSpace(rawInput))
            {
                return parsedCommands;
            }

            if (!rawInput.Contains("&&"))
            {
                parsedCommands.Add(new CommandName { RawString = rawInput});
            }
            else
            {
                var multiCommands = rawInput.Split(new[] { "&&" }, StringSplitOptions.None);
                if (!multiCommands.Any())
                {
                    return parsedCommands;
                }
                foreach (var rawCommand in multiCommands)
                {
                    parsedCommands.Add(new CommandName { RawString = rawCommand });
                }

            }

            foreach (var parsedCommand in parsedCommands)
            {
                string name;
                string cleanedString;
                try
                {
                    while (parsedCommand.RawString.IndexOf(' ') == 0)
                    {
                        parsedCommand.RawString = parsedCommand.RawString.TrimStart(' ');
                    }


                    int firstWhitespace = parsedCommand.RawString.IndexOf(' ');

                    if (firstWhitespace == -1)
                    {
                        parsedCommand.Name = parsedCommand.RawString;
                        parsedCommand.CleanInput = String.Empty;
                        parsedCommand.Success = true;
                        continue;
                    }

                    name = rawInput.Substring(0, firstWhitespace + 1);
                    if (String.IsNullOrWhiteSpace(name))
                    {
                        return parsedCommands;
                    }
                    cleanedString = parsedCommand.RawString.Replace(name + " ", "");
                    name = name.TrimEnd(' ');
                    name = name.TrimStart(' ');
                    parsedCommand.Name = name;
                    parsedCommand.CleanInput = cleanedString;
                    parsedCommand.Success = true;
                }
                catch
                {
                    //TODO Abendteuerlich oder ?
                    parsedCommand.Success = false;
                    return parsedCommands;
                }
            }
            return parsedCommands;
        }

        public ValidatedCommand ValidateCommand(CommandName commandName)
        {
            var command = _allCommands.FirstOrDefault(cn => cn.Name.ToLower().Equals(commandName.Name.ToLower()));
            if (command == null)
            {
                return null;
            }
            var clone = CreateCleanCommandClone(command);
            return new ValidatedCommand { FoundCommand = command, CloneCommand = clone };
        }

        public IList<Option> GetOptions(CommandName name, Command command)
        {
            if (!name.CleanInput.Contains('-'))
            {
                return null;
            }
            var rawOptions = name.CleanInput.Split('-');
            if (!rawOptions.Any())
            {
                return null;
            }
            var options = new List<Option>();
            foreach (var rawOption in rawOptions)
            {
                if (string.IsNullOrWhiteSpace(rawOption))
                {
                    continue;
                }
                string optionName = String.Empty;
                try
                {
                    int firstWhitespace = rawOption.IndexOf(' ');
                    if (firstWhitespace == -1)
                    {
                        optionName = rawOption;
                    }
                    else
                    {
                        optionName = rawOption.Substring(0, firstWhitespace);
                    }
                }
                catch
                {
                    continue;
                }

                var cOption =
                        command.Options.FirstOrDefault(c => c.Identifier.ToLower().Equals(optionName.ToLower()));
                if (cOption == null)
                {
                    continue;
                }
                var clone = new Option();
                clone = Mapper.Map(cOption, clone);
                if (!cOption.HaveParameters)
                {
                    options.Add(clone);
                    continue;
                }
                var parameters = GetParameters(rawOption.Replace(optionName + " ", ""));
                if (parameters == null)
                {
                    options.Add(clone);
                    continue;
                }
                clone.Parameters = parameters;
                options.Add(clone);

            }
            return options;
        }

        public IList<Parameter> GetParameters(string cleanParameterString)
        {
            if (String.IsNullOrWhiteSpace(cleanParameterString))
            {
                return null;
            }
            IList<Parameter> parameters = new List<Parameter>();

            if (cleanParameterString.Contains("||"))
            {
                var rawParameters = cleanParameterString.Split(new[] { "||" }, StringSplitOptions.None);
                if (!rawParameters.Any())
                {
                    return parameters;
                }
                foreach (var rawParameter in rawParameters)
                {
                    parameters.Add(new Parameter { RawValue = rawParameter });
                }
            }
            else
            {
                parameters.Add(new Parameter { RawValue = cleanParameterString });
            }
            return parameters;
        }

        public Command CreateCleanCommandClone(Command command)
        {
            var clone = new Command();
            clone = Mapper.Map(command, clone);
            return clone;
        }
        #endregion

    }

    public class CommandName
    {
        public string Name { get; set; }
        public string CleanInput { get; set; }
        public string RawString { get; set; }
        public bool Success { get; set; }
    }

    public class ValidatedCommand
    {
        public Command FoundCommand { get; set; }
        public Command CloneCommand { get; set; }
    }

}
