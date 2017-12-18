using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Nut.CommandLineParser.Models
{
    internal class PropertyOptionPairs : IEnumerable<PropertyOptionPair>
    {
        private List<PropertyOptionPair> collection;

        public PropertyOptionPairs()
        {
            this.collection = new List<PropertyOptionPair>();
        }

        public PropertyOptionPairs(IEnumerable<PropertyOptionPair> collection)
        {
            this.collection = new List<PropertyOptionPair>(collection);
        }

        public int Count => this.collection.Count;

        public PropertyOptionPair this[int index] => this.collection[index];

        public IEnumerator<PropertyOptionPair> GetEnumerator()
        {
            return ((IEnumerable<PropertyOptionPair>)collection).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<PropertyOptionPair>)collection).GetEnumerator();
        }

        internal bool TryFindByOption(string option, out PropertyInfo property)
        {
            foreach (var item in this.collection)
            {
                if (item.Option.Equals(option, StringComparison.Ordinal))
                {
                    property = item.Property;
                    return true;
                }
            }

            property = null;
            return false;
        }
    }
}