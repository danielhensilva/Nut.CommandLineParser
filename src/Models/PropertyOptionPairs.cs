using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Nut.CommandLineParser.Models
{
    internal class PropertyOptionPairs : IEnumerable<PropertyOptionPair>
    {
        private readonly List<PropertyOptionPair> _collection;

        public PropertyOptionPairs()
        {
            _collection = new List<PropertyOptionPair>();
        }

        public PropertyOptionPairs(IEnumerable<PropertyOptionPair> collection)
        {
            _collection = new List<PropertyOptionPair>(collection);
        }

        public int Count => _collection.Count;

        public PropertyOptionPair this[int index] => _collection[index];

        public IEnumerator<PropertyOptionPair> GetEnumerator()
        {
            return ((IEnumerable<PropertyOptionPair>) _collection).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<PropertyOptionPair>) _collection).GetEnumerator();
        }

        internal string[] GetDuplicatedOptions()
        {
            var duplicated = new HashSet<string>();
            var options = new HashSet<string>();

            foreach (var item in _collection)
            {
                if (options.Add(item.Option))
                    continue;

                duplicated.Add(item.Option);
            }

            return duplicated.ToArray();
        }

        internal bool TryFindByOption(string option, out PropertyInfo property)
        {
            foreach (var item in _collection)
            {
                if (!item.Option.Equals(option, StringComparison.Ordinal))
                    continue;

                property = item.Property;
                return true;
            }

            property = null;
            return false;
        }
    }
}