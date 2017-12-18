using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Nut.CommandLineParser.Models
{
    public class ArgKeyValuePairs : IEnumerable<ArgKeyValuePair>
    {
        private List<ArgKeyValuePair> collection;

        public ArgKeyValuePairs()
        {
            this.collection = new List<ArgKeyValuePair>();
        }
        
        public ArgKeyValuePairs(IEnumerable<ArgKeyValuePair> collection)
        {
            this.collection = new List<ArgKeyValuePair>(collection);
        }

        public int Count => this.collection.Count;

        public ArgKeyValuePair this[int index] => this.collection[index];

        public IEnumerator<ArgKeyValuePair> GetEnumerator()
        {
            return ((IEnumerable<ArgKeyValuePair>)collection).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<ArgKeyValuePair>)collection).GetEnumerator();
        }
    }
}