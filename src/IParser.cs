using System.Runtime.CompilerServices;
using Nut.CommandLineParser.Models;

[assembly: InternalsVisibleTo("Nut.CommandLineParser.Test")]

namespace Nut.CommandLineParser
{
    public interface IParser
    {
        ArgKeyValuePairs ParseToKeyValuePairs(params string[] args);

        TElement ParseToObject<TElement>(params string[] args) where TElement : new();
    }
}