using System;
using System.Reflection;

namespace Nut.CommandLineParser.Models
{
    internal class PropertyOptionPair
    {
        private PropertyInfo _property;

        public PropertyInfo Property
        {
            get => _property;
            set => _property = value ?? throw new ArgumentNullException(nameof(Property));
        }

        public string Option { get; set; }

        public PropertyOptionPair(PropertyInfo property, string option)
        {
            Property = property;
            Option = option;   
        }
    }
}