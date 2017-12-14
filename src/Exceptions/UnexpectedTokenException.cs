using System;

namespace Nut.CommandLineParser.Exceptions
{
    public class UnexpectedTokenException : Exception
    {
        public int Index { get; set; }

        public string Token { get; set; }

        public UnexpectedTokenException(int index, string token)
        {
            this.Index = index;
            this.Token = token;
        }
    }
}