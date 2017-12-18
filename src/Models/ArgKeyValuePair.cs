using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Nut.CommandLineParser.Models
{
    public class ArgKeyValuePair
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public ArgKeyValuePair()
        {
        }

        public ArgKeyValuePair(string key, string value)
        {
            this.Key = key;
            this.Value = value;   
        }
    }
}