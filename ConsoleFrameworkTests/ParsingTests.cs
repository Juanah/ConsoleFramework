using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using ConsoleFramework;
using ConsoleFramework.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConsoleFrameworkTests
{
    /// <summary>
    /// Summary description for ParsingTests
    /// </summary>
    [TestClass]
    public class ParsingTests
    {
        private CommandParser _parser;
        private Random random = new Random();
        public ParsingTests()
        {
        }

        [TestMethod]
        public void NameParsingTest()
        {
            var randomCommands = CreateRandomCommands(2000);
            _parser = new CommandParser(randomCommands.ToList());
            
            foreach (var randomCommand in randomCommands)
            {
                var whiteSpaceFront = " ";
                var whiteSpaceBack = " ";

                if (GetRandomBoolean())
                {
                    whiteSpaceFront = "";
                }
                if (GetRandomBoolean())
                {
                    whiteSpaceBack = "";
                }
                var fakeCommand = String.Format("{0}{1}{2}",whiteSpaceFront,randomCommand.Name,whiteSpaceBack);
                bool multiple = GetRandomBoolean();
                while (multiple)
                {
                    if (whiteSpaceBack == " ")
                    {
                        fakeCommand += fakeCommand;
                    }
                    else
                    {
                        fakeCommand += " " + fakeCommand;
                    }
                    multiple = GetRandomBoolean();
                }
                var names = _parser.GetCommandNames(fakeCommand);
                Assert.AreNotEqual(null,names);
                Assert.AreEqual(true,names.Any());
                Assert.AreEqual(randomCommand.Name,names[0].Name);
            }

        }

        public bool GetRandomBoolean()
        {
            return random.Next(0, 2) == 0;
        }

        #region Creations

        IList<Command> CreateRandomCommands(int size)
        {
            IList<Command> commands = new List<Command>();

            for (int i = 0; i < size; i++)
            {
                string name = "".GetRandomString(6);
                var tempCommand = new Command(name, CaseLevel.Low);
                commands.Add(tempCommand);
            }
            return commands;
        }

        #endregion

    }
}
