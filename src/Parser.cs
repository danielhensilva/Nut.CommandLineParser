using Nut.CommandLineParser.Models;
using Nut.CommandLineParser.Specialized;

namespace Nut.CommandLineParser
{
    public class Parser : IParser
    {
        public ArgKeyValuePairs ParseToKeyValuePairs(string args)
        {
            return new KeyValuePairParser().Parse(args);
        }

        public TElement ParseToObject<TElement>(string args) where TElement : new()
        {
            return new ObjectParser<TElement>().Parse(args);
        }
    }
}