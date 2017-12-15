using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Nut.CommandLineParser.Specialized;

namespace Nut.CommandLineParser 
{
    public class Parser : IParser
    {
        public KeyValuePair<string, string>[] ParseToKeyValuePairs(string args) 
        {
            return new KeyValuePairParser().Parse(args);
        }

        public TElement ParseToObject<TElement>(string args) where TElement : new()
        {
            return new ObjectParser<TElement>().Parse(args);
        }
    }
}