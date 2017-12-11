using System;

namespace Nut.CommandLineParser.Exceptions
{
    public class UnexpectedTokenException : Exception
    {
        public string Token { get; set; }

        public UnexpectedTokenException(string token)
        {
            this.Token = token;
        }
    }
}