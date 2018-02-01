using System;

namespace Nut.CommandLineParser.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class OptionNameAttribute : Attribute, IOptionAttribute
    {
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

            Name = name;
        }

        public string Name { get; }

        public string GetValue()
        {
            return Name;
        }
    }
}