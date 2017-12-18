using System;
using Nut.CommandLineParser.Extensions;

namespace Nut.CommandLineParser.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=true, Inherited=false)]
    public class OptionNameAttribute : Attribute, IOptionAttribute
    {
        public string Name { get; }

        public string GetValue() => this.Name.ToString();

        public OptionNameAttribute(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            name = name.Trim();

            if (name.Length == 0)
                throw new ArgumentException("Value cannot be empty.", nameof(name));

            if (name.Length == 1)
                throw new ArgumentException(
                    $"Value cannot be one char length. Consider using {nameof(OptionAliasAttribute)} class instead.",
                    nameof(name));

            this.Name = name;
        }
    }
}