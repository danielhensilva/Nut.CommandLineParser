using System.Runtime.CompilerServices;
using Nut.CommandLineParser.Models;

[assembly: InternalsVisibleTo("Nut.CommandLineParser.Test")]

namespace Nut.CommandLineParser
{
    public interface IParser
    {
        ArgKeyValuePairs ParseToKeyValuePairs(string args);

        TElement ParseToObject<TElement>(string args) where TElement : new();
    }
}