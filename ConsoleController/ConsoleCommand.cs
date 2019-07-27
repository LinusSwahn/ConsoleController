using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ConsoleController.Parsing;

namespace ConsoleController
{
    public abstract class ConsoleCommand : IConsoleCommand
    {
        private ITypeParsingHelper _parsingHelper;
        
        private Dictionary<string, MethodInfo> _methods;

        protected ConsoleCommand(ITypeParsingHelper parsingHelper)
        {
            _parsingHelper = parsingHelper;
            
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
                var methodParameters = method.GetParameters();
                var inputParameters = command.Skip(1).ToArray();

                if (methodParameters.Length != inputParameters.Length)
                {
                    return Help();
                }

                var parameters = new object[methodParameters.Length];
                
                for (int i = 0; i < methodParameters.Length; i++)
                {
                    parameters[i] = _parsingHelper.Parse(methodParameters[i].ParameterType, inputParameters[i]);
                }
                
                return method.Invoke(this, parameters) as string;
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