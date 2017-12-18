using System;
using Nut.CommandLineParser.Extensions;

namespace Nut.CommandLineParser.Exceptions
{
    public class UnboundTokenException : Exception
    {
        public string Token { get; }

        public UnboundTokenException(string token)
            : this(token, UnboundTokenException.GenerateMessage(token))
        {
        }

        private UnboundTokenException(string token, string message)
            : base(message) 
        {
            this.Token = token;
        }

        private static string GenerateMessage(string token)
        {
            if (token == null)
                throw new ArgumentNullException(nameof(token));

            if (token.IsEmptyOrWhitespace())
                throw new ArgumentException("Value cannot be empty.", nameof(token));

            return $"Unbound token {token}.";
        }
    }
}