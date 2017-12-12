using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Nut.CommandLineParser;
using Nut.CommandLineParser.Exceptions;
using Nut.CommandLineParser.Extensions;

namespace Nut.CommandLineParser.Specialized
{
    public class KeyValuePairParser : ISpecializedParser<KeyValuePair<string, string>[]>
    {
        public KeyValuePair<string, string>[] Parse(string args) 
        {
            if (args == null) 
                throw new ArgumentNullException(nameof(args));

            if (args.IsEmptyOrWhitespace())
                return new KeyValuePair<string, string>[0];

            var pattern = @"(--(\w+?) {1,}(\w+?)\b|-(\w+?) {1,}(\w+?)\b|\b(\w+?)=(\w+?)\b|\b(\w+)\b)";
            var matches = Regex.Matches(args, pattern);

            var count = matches.Count;
            var pairs = new KeyValuePair<string, string>[count];

            for (var i = 0; i < count; i++)
            {
                var groups = matches[i].Groups;

                if (groups[2].Value != string.Empty)
                {
                    pairs[i] = new KeyValuePair<string, string>(
                        key: groups[2].Value,
                        value: groups[3].Value
                    );
                }
                else if (groups[4].Value != string.Empty)
                {
                    pairs[i] = new KeyValuePair<string, string>(
                        key: groups[4].Value,
                        value: groups[5].Value
                    );
                }
                else if (groups[6].Value != string.Empty) 
                {
                    pairs[i] = new KeyValuePair<string, string>(
                        key: groups[6].Value,
                        value: groups[7].Value
                    );
                }
                else if (groups[8].Value != string.Empty) 
                {
                    pairs[i] = new KeyValuePair<string, string>(
                        key: groups[8].Value,
                        value: null
                    );
                }

                args = args.RemoveFirstOccurrence(groups[1].Value);
            }

            if (args.IsEmptyOrWhitespace())
                return pairs;
            
            var token = args.FirstWordOrDefault();
            throw new UnexpectedTokenException(token);
        }
    }
}