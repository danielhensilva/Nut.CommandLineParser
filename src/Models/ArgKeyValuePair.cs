using System;

namespace Nut.CommandLineParser.Models
{
    public class ArgKeyValuePair
    {
        private string _key;

        public ArgKeyValuePair(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key
        {
            get => _key;
            set => _key = value ?? throw new ArgumentNullException("key");
        }

        public string Value { get; set; }
    }
}