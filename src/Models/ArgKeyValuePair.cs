using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Nut.CommandLineParser.Models
{
    public class ArgKeyValuePair
    {
        private string key;

        public string Key
        {
            get => this.key;
            set
            { 
                if (value == null)
                    throw new ArgumentNullException(nameof(key));

                this.key = value;
            }
        }

        public string Value { get; set; }

        public ArgKeyValuePair(string key, string value)
        {
            this.Key = key;
            this.Value = value;   
        }
    }
}