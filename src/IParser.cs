using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("Nut.CommandLineParser.Test")]
namespace Nut.CommandLineParser 
{
    public interface IParser
    {
        KeyValuePair<string, string>[] ParseToKeyValuePairs(string args);
        
        TElement ParseToObject<TElement>(string args) where TElement : new();
    }
}