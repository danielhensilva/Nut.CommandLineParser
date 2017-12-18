using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Nut.CommandLineParser.Attributes;

namespace Nut.CommandLineParser.Specialized
{
    internal class ObjectParser<TElement> : ISpecializedParser<TElement> where TElement : new()
    {
        public TElement Parse(string args) 
        {
            return new TElement();   
        }
    }
}