using System;
using System.Collections.Generic;

namespace ConsoleController.Parsing
{
    public class TypeParsingHelper : ITypeParsingHelper
    {
        private Dictionary<Type, Func<string, object>> _parsers;

        public TypeParsingHelper(Dictionary<Type, Func<string, object>> parsers)
        {
            _parsers = parsers;
        }

        public object Parse(Type type, string input)
        {
            if (_parsers.TryGetValue(type, out var parser))
            {
                return parser(input);
            }

            throw new NotImplementedException($"No parser registered for type: '{type}'");
        }
    }
}