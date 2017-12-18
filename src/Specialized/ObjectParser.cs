using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Nut.CommandLineParser.Attributes;
using Nut.CommandLineParser.Exceptions;

namespace Nut.CommandLineParser.Specialized
{
    internal class ObjectParser<TElement> : ISpecializedParser<TElement> where TElement : new()
    {
        public TElement Parse(string args) 
        {
            var rawParser = new KeyValuePairParser();
            var keyValuePairs = rawParser.Parse(args);

            if (keyValuePairs.Length == 0)
                return new TElement();
            
            var propertyOptionPairs = ReadOptionsFromAttributes();

            var element = new TElement();

            foreach (var keyValue in keyValuePairs)
            {
                bool bound = false;

                foreach (var propertyOption in propertyOptionPairs)
                {
                    if (propertyOption.Value.Equals(keyValue.Key, StringComparison.Ordinal))
                    {
                        propertyOption.Key.SetValue(element, keyValue.Value);
                        bound = true;
                        break;
                    }
                }

                if (!bound)
                    throw new UnboundTokenException(keyValue.Key);
            }

            return element;
        }

        private static KeyValuePair<PropertyInfo, string>[] ReadOptionsFromAttributes()
        {
            var query =
                from propertyInfo in typeof(TElement).GetProperties()
                from propertyAttribute in propertyInfo.GetCustomAttributes()
                where typeof(IOptionAttribute).IsAssignableFrom(propertyAttribute.GetType())
                let optionAttribute = propertyAttribute as IOptionAttribute
                let propertyOption = optionAttribute.GetValue()
                select new KeyValuePair<PropertyInfo, string>(propertyInfo, propertyOption);

            return query.ToArray();
        }
    }
}