using System.Collections;
using System.Collections.Generic;

namespace Nut.CommandLineParser.Models
{
    public class ArgKeyValuePairs : IEnumerable<ArgKeyValuePair>
    {
        private readonly List<ArgKeyValuePair> _collection;

        public ArgKeyValuePairs()
        {
            _collection = new List<ArgKeyValuePair>();
        }

        public ArgKeyValuePairs(IEnumerable<ArgKeyValuePair> collection)
        {
            _collection = new List<ArgKeyValuePair>(collection);
        }

        public int Count => _collection.Count;

        public ArgKeyValuePair this[int index] => _collection[index];

        public IEnumerator<ArgKeyValuePair> GetEnumerator()
        {
            return ((IEnumerable<ArgKeyValuePair>) _collection).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<ArgKeyValuePair>) _collection).GetEnumerator();
        }
    }
}