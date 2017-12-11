using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("Nut.CommandLineParser.Test")]
namespace Nut.CommandLineParser 
{
    public interface IParser
    {
        KeyValuePair<string, string> ParseToKeyValuePair(string args);
        
        TValue ParseToObject<TValue>(string args);
    }
}