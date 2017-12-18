using System;
using Nut.CommandLineParser.Extensions;

namespace Nut.CommandLineParser.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=true, Inherited=false)]
    public class OptionAliasAttribute : Attribute, IOptionAttribute
    {
        public char Alias { get; }

        public string GetValue() => this.Alias.ToString();

        public OptionAliasAttribute(char alias)
        {
            if (!char.IsLetterOrDigit(alias))
                throw new ArgumentException("Value can only be letter or digit.", nameof(alias));

            this.Alias = alias;
        }
    }
}