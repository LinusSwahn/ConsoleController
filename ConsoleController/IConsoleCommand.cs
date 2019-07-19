using System.Collections.Generic;

namespace ConsoleController
{
    public interface IConsoleCommand
    {
        string Parse(List<string> command);
        string Default();
        string Help();
    }
}