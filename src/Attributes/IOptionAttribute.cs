using System;

namespace Nut.CommandLineParser
{
    public interface IOptionAttribute
    {
        string GetValue();
    }
}