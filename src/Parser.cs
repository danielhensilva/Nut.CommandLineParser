using Nut.CommandLineParser.Models;
using Nut.CommandLineParser.Specialized;

namespace Nut.CommandLineParser
{
    public class Parser : IParser
    {
        public ArgKeyValuePairs ParseToKeyValuePairs(params string[] args)
        {
            var parser = new KeyValuePairParser();
            var jointArgs = string.Join(" ", args);
            return parser.Parse(jointArgs);
        }

        public TElement ParseToObject<TElement>(params string[] args) where TElement : new()
        {
            var parser = new ObjectParser<TElement>();
            var jointArgs = string.Join(" ", args);
            return parser.Parse(jointArgs);
        }
    }
}