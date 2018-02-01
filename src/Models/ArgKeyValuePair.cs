using System;

namespace Nut.CommandLineParser.Models
{
    public class ArgKeyValuePair
    {
        private string key;

        public ArgKeyValuePair(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key
        {
            get => key;
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(key));

                key = value;
            }
        }

        public string Value { get; set; }
    }
}