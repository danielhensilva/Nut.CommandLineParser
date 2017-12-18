using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Nut.CommandLineParser.Models
{
    public class PropertyOptionPair
    {
        public PropertyInfo Property { get; set; }

        public string Option { get; set; }

        public PropertyOptionPair()
        {
        }

        public PropertyOptionPair(PropertyInfo property, string option)
        {
            this.Property = property;
            this.Option = option;   
        }
    }
}