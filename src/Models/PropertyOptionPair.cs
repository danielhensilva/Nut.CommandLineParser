using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Nut.CommandLineParser.Models
{
    internal class PropertyOptionPair
    {
        private PropertyInfo property;

        public PropertyInfo Property
        {
            get => this.property;
            set 
            { 
                if (value == null)
                    throw new ArgumentNullException(nameof(property));

                this.property = value;
            }
        }

        public string Option { get; set; }

        public PropertyOptionPair(PropertyInfo property, string option)
        {
            this.Property = property;
            this.Option = option;   
        }
    }
}