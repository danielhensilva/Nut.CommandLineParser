using System;

namespace Nut.CommandLineParser.Extensions 
{
    internal static class StringExtensions 
    {
        internal static bool IsEmptyOrWhitespace(this string value) 
        {
            if (value == null)
                return false;

            if (value.Trim().Length > 0)
                return false;

            return true;
        }

        internal static string RemoveAll(this string target, string value)
        {
            if (string.IsNullOrEmpty(target))
                return target;

            if (value == null)
                return target;

            return target.Replace(value, string.Empty);
        }

        internal static string RemoveFirstOccurrence(this string target, string value) 
        {
            if (string.IsNullOrEmpty(target))
                return target;

            if (value == null)
                return target;

            var index = target.IndexOf(value);
            var count = value.Length;

            return target.Remove(index, count);
        }

        internal static string RemoveLastOccurrence(this string target, string value)
        {
            if (string.IsNullOrEmpty(target))
                return target;

            if (value == null)
                return target;

            var index = target.LastIndexOf(value);
            var count = value.Length;

            return target.Remove(index, count);
        }

        internal static string FirstWordOrDefault(this string target) 
        {
            if (string.IsNullOrWhiteSpace(target))
                return null;

            var separator = new[] {" "};
            var splitOptions = StringSplitOptions.RemoveEmptyEntries;
            var words = target.Trim().Split(separator, splitOptions);

            if (words.Length == 0)
                return null;

            return words[0];
        }
    }    
}