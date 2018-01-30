using System;
using System.Linq;
using System.Reflection;
using Nut.CommandLineParser.Extensions;
using Nut.CommandLineParser.Exceptions;
using Nut.CommandLineParser.Models;

namespace Nut.CommandLineParser.Specialized
{
    internal class ObjectParser<TElement> : ISpecializedParser<TElement> where TElement : new()
    {
        public TElement Parse(string args)
        {
            var properties = GetPropertyOptionPairs();
            
            var pairs = new KeyValuePairParser().Parse(args);
            if (pairs.Count == 0)
                return new TElement();

            var element = CreateElement(pairs, properties);
            return element;
        }

        private static TElement CreateElement(ArgKeyValuePairs pairs, PropertyOptionPairs properties) 
        {
            var element = new TElement();

            foreach (var pair in pairs)
            {
                if (!properties.TryFindByOption(pair.Key, out var property))
                    throw new UnboundTokenException(pair.Key);

                var parsedValue = pair.Value.Parse(property.PropertyType);
                property.SetValue(element, parsedValue);
            }

            return element;
        }

        private static PropertyOptionPairs GetPropertyOptionPairs()
        {
            var query =
                from property in typeof(TElement).GetProperties()
                from propertyAttribute in property.GetCustomAttributes()
                where propertyAttribute is IOptionAttribute
                let optionAttribute = propertyAttribute as IOptionAttribute
                let option = optionAttribute.GetValue()
                select new PropertyOptionPair(property, option);

            var collection = new PropertyOptionPairs(query);

            var duplicated = collection.GetDuplicatedOptions();
            if (duplicated.Any())
                throw new DuplicatedOptionsException(duplicated);            
            
            return collection;
        }
    }
}