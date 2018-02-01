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

            var index = target.IndexOf(value, StringComparison.Ordinal);
            var count = value.Length;

            return target.Remove(index, count);
        }

        internal static string RemoveLastOccurrence(this string target, string value)
        {
            if (string.IsNullOrEmpty(target))
                return target;

            if (value == null)
                return target;

            var index = target.LastIndexOf(value, StringComparison.Ordinal);
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

        internal static object Parse(this string value, Type type)
        {
            if (type == typeof(string)) 
                return Convert.ChangeType(value, type);

            if (type == typeof(decimal))
            {
                if (decimal.TryParse(value, out var newDecimalValue))
                    return newDecimalValue;
            }
            else if (type == typeof(bool))
            {
                if (bool.TryParse(value, out var newBooleanValue))
                    return newBooleanValue;

                if (int.TryParse(value, out var newIntegerValue))
                {
                    if (newIntegerValue == 0)
                        return false;

                    if (newIntegerValue == 1)
                        return true;
                }
            }
            else if (type.IsPrimitive)
            {
                return Convert.ChangeType(value, type);
            }

            var errorMessage = $@"""{value}"" cannot be casted to type {type.FullName}";
            throw new InvalidCastException(errorMessage);
        }
    }
}