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
                if (matches[i].Groups[2].Value != "")
                {
                    pairs[i] = new KeyValuePair<string, string>(
                        key: matches[i].Groups[2].Value,
                        value: matches[i].Groups[3].Value
                    );
                }

                if (matches[i].Groups[4].Value != "")
                {
                    pairs[i] = new KeyValuePair<string, string>(
                        key: matches[i].Groups[4].Value,
                        value: matches[i].Groups[5].Value
                    );
                }

                if (matches[i].Groups[6].Value != "") 
                {
                    pairs[i] = new KeyValuePair<string, string>(
                        key: matches[i].Groups[6].Value,
                        value: matches[i].Groups[7].Value
                    );
                }

                if (matches[i].Groups[8].Value != "") 
                {
                    pairs[i] = new KeyValuePair<string, string>(
                        key: matches[i].Groups[8].Value,
                        value: null
                    );
                }

                var fullMatch = matches[i].Groups[0].Value;
                args = args.RemoveFirstOccurrence(fullMatch);
            }

            if (args.IsEmptyOrWhitespace())
                return pairs;
            
            var token = args.FirstWordOrDefault();
            throw new UnexpectedTokenException(token);
        }
    }
}