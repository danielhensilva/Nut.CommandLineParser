using System;

namespace Nut.CommandLineParser.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class OptionAliasAttribute : Attribute, IOptionAttribute
    {
        public OptionAliasAttribute(char alias)
        {
            if (!char.IsLetterOrDigit(alias))
                throw new ArgumentException("Value can only be letter or digit.", nameof(alias));

            Alias = alias;
        }

        public char Alias { get; }

        public string GetValue() => Alias.ToString();
    }
}