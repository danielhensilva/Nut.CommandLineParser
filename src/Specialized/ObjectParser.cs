using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Nut.CommandLineParser.Attributes;
using Nut.CommandLineParser.Exceptions;
using Nut.CommandLineParser.Models;

namespace Nut.CommandLineParser.Specialized
{
    internal class ObjectParser<TElement> : ISpecializedParser<TElement> where TElement : new()
    {
        public TElement Parse(string args) 
        {
            var pairs = new KeyValuePairParser().Parse(args);
            if (pairs.Count == 0)
                return new TElement();

            var properties = this.GetPropertyOptionPairs();
            var element = this.CreateElement(pairs, properties);
            return element;
        }

        private TElement CreateElement(ArgKeyValuePairs pairs, PropertyOptionPairs properties) 
        {
            var element = new TElement();

            foreach (var pair in pairs)
            {
                if (!properties.TryFindByOption(pair.Key, out PropertyInfo property))
                    throw new UnboundTokenException(pair.Key);

                property.SetValue(element, pair.Value);
            }

            return element;
        }

        private PropertyOptionPairs GetPropertyOptionPairs()
        {
            var query =
                from property in typeof(TElement).GetProperties()
                from propertyAttribute in property.GetCustomAttributes()
                where typeof(IOptionAttribute).IsAssignableFrom(propertyAttribute.GetType())
                let optionAttribute = propertyAttribute as IOptionAttribute
                let option = optionAttribute.GetValue()
                select new PropertyOptionPair(property, option);

            var collection = new PropertyOptionPairs(query);
            return collection;
        }
    }
}