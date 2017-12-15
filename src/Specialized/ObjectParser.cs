using System;

namespace Nut.CommandLineParser.Specialized
{
    public class ObjectParser<TElement> : ISpecializedParser<TElement> where TElement : new()
    {
        public TElement Parse(string args) 
        {
            return new TElement();
        }
    }
}