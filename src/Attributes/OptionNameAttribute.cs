using System;
using Nut.CommandLineParser.Extensions;

namespace Nut.CommandLineParser.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=true, Inherited=false)]
    public class OptionNameAttribute : Attribute
    {
        public string Name { get; }

        public OptionNameAttribute(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (name.IsEmptyOrWhitespace())
                throw new ArgumentException("Value cannot be empty.", nameof(name));

            this.Name = name;
        }
    }
}