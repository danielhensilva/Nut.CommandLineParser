using System.Collections;
using System.Collections.Generic;

namespace Nut.CommandLineParser.Models
{
    public class ArgKeyValuePairs : IEnumerable<ArgKeyValuePair>
    {
        private readonly List<ArgKeyValuePair> collection;

        public ArgKeyValuePairs()
        {
            collection = new List<ArgKeyValuePair>();
        }

        public ArgKeyValuePairs(IEnumerable<ArgKeyValuePair> collection)
        {
            this.collection = new List<ArgKeyValuePair>(collection);
        }

        public int Count => collection.Count;

        public ArgKeyValuePair this[int index] => collection[index];

        public IEnumerator<ArgKeyValuePair> GetEnumerator()
        {
            return ((IEnumerable<ArgKeyValuePair>) collection).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<ArgKeyValuePair>) collection).GetEnumerator();
        }
    }
}