using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ConsoleController
{
    public abstract class ConsoleCommand : IConsoleCommand
    {
        private Dictionary<string, MethodInfo> _methods;

        protected ConsoleCommand()
        {
            _methods = GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                .ToDictionary(m => m.Name.ToLower());
        }
        
        public string Parse(List<string> command)
        {
            if (!command.Any() || !_methods.ContainsKey(command.First().ToLower()))
            {
                return Default();
            }

            if (_methods.TryGetValue(command.First().ToLower(), out var method))
            {
                return method.Invoke(this, new[] {command.Skip(1).ToArray()}) as string;
            }

            return Help();
        }

        public virtual string Default()
        {
            return Help();
        }

        public virtual string Help()
        {
            return "Available methods for this class are: \n" + string.Join("n", _methods.Keys);
        }
    }
}