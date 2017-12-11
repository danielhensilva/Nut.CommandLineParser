using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Nut.CommandLineParser.Specialized;

[assembly:InternalsVisibleTo("Nut.CommandLineParser.Test")]
namespace Nut.CommandLineParser 
{
    public static class Parser
    {
        public static KeyValuePair<string, string>[] ParseToKeyValuePairs(string args) 
        {
            return new KeyValuePairParser().Parse(args);
        }
    }
}