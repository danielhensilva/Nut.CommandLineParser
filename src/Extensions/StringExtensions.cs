using System;

namespace Nut.CommandLineParser.Extensions 
{
    public static class StringExtensions 
    {
        public static bool IsEmptyOrWhitespace(this string value) 
        {
            if (value == null)
                return false;

            if (value.Trim().Length > 0)
                return false;

            return true;
        }

        public static string RemoveAll(this string target, string value)
        {
            if (string.IsNullOrEmpty(target))
                return target;

            if (value == null)
                return target;

            return target.Replace(value, string.Empty);
        }

        public static string RemoveFirstOccurrence(this string target, string value) 
        {
            if (string.IsNullOrEmpty(target))
                return target;

            if (value == null)
                return target;

            var index = target.IndexOf(value);
            var count = value.Length;

            return target.Remove(index, count);
        }

        public static string RemoveLastOccurrence(this string target, string value)
        {
            if (string.IsNullOrEmpty(target))
                return target;

            if (value == null)
                return target;

            var index = target.LastIndexOf(value);
            var count = value.Length;

            return target.Remove(index, count);
        }

        public static string FirstWordOrDefault(this string target) 
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