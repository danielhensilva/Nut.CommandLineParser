using System;
using System.Linq;

namespace Nut.CommandLineParser 
{
    internal interface ISpecializedParser<TElement>
    {
        TElement Parse(string args);
    }
}