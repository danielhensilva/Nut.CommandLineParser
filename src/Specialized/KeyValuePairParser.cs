using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Nut.CommandLineParser.Models;
using Nut.CommandLineParser.Exceptions;
using Nut.CommandLineParser.Extensions;

namespace Nut.CommandLineParser.Specialized
{
    internal class KeyValuePairParser : ISpecializedParser<ArgKeyValuePairs>
    {
        public ArgKeyValuePairs Parse(string args) 
        {
            if (args == null) 
                throw new ArgumentNullException(nameof(args));

            if (args.IsEmptyOrWhitespace())
                return new ArgKeyValuePairs();

            var parsedTokens = ParseTokens(args);
            var collection = new ArgKeyValuePairs(parsedTokens);
            return collection;
        }

        private static IEnumerable<ArgKeyValuePair> ParseTokens(string args) 
        {
            var currentArgs = args;

            while(true)
            {
                if (currentArgs.IsEmptyOrWhitespace())
                    yield break;

                var keyValue = ParseToken(currentArgs, out var match);
                if (keyValue == null) 
                    break;
                
                currentArgs = currentArgs.Remove(0, match.Length);
                yield return keyValue;
            }

            currentArgs = currentArgs.Trim();

            var index = args.IndexOf(currentArgs, StringComparison.Ordinal);
            var token = currentArgs.FirstWordOrDefault();
            throw new UnexpectedTokenException(index, token);
        }
                
        private static ArgKeyValuePair ParseToken(string args, out string match) 
        {
            match = null;

            string[] patterns = 
            {
                @"^\s*--(\w+?)\s+""(.+?)""",
                @"^\s*-(\w{1})\s+""(.+?)""",
                @"^\s*\b(\w+?)=""(.+?)""",
                @"^\s*""(.+?)""",
                @"^\s*--(\w+?)\s+(\w+?)\b",
                @"^\s*-(\w{1})\s+(\w+?)\b",
                @"^\s*\b(\w+?)=(\w+?)\b",
                @"^\s*\b(\w+)\b",
            };

            foreach (var pattern in patterns)
            {
                if (MatchNext(args, pattern, out match, out var key, out var value)) 
                {
                    return new ArgKeyValuePair(key, value);
                }
            }
            
            return null;
        }

        private static bool MatchNext(string args, string pattern, out string match, out string key, out string value) 
        {
            key = null;
            value = null;
            match = null;

            var regex = new Regex(pattern);
            var regexMatch = regex.Match(args);
            
            if (!regexMatch.Success)
                return false;

            if (regexMatch.Groups == null)
                return false;
                
            if (regexMatch.Groups.Count == 0)
                return false;

            if (regexMatch.Groups.Count > 0)
                match = regexMatch.Groups[0].Value;

            if (regexMatch.Groups.Count > 1)
                key = regexMatch.Groups[1].Value;

            if (regexMatch.Groups.Count > 2)
                value = regexMatch.Groups[2].Value;

            return true;
        }
    }
}