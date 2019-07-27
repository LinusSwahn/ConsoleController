using System.Collections.Generic;
using System.Linq;

namespace ConsoleController.Parsing
{
    public class CommandParser : ICommandParser
    {
        private Dictionary<string, IConsoleCommand> _commands;

        public CommandParser(IEnumerable<IConsoleCommand> commands)
        {
            _commands = commands.ToDictionary(c => c.GetType().Name.ToLower());
        }

        public string ParseCommand(string command)
        {
            var splitCommand = command.Split(' ').Where(s => s != ""); 
            if (_commands.TryGetValue(splitCommand.First().ToLower(), out var consoleCommand))
            {
                return consoleCommand.Parse(splitCommand.Skip(1).ToList());
            }

            return "Command not found.";
        }
    }
}