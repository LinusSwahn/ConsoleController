using System;

namespace ConsoleController.Parsing
{
    public interface ITypeParsingHelper
    {
        object Parse(Type type, string input);
    }
}