using System;
using Nut.CommandLineParser.Extensions;

namespace Nut.CommandLineParser.Exceptions
{
    public class UnboundTokenException : Exception
    {
        public UnboundTokenException(string token)
            : this(token, GenerateMessage(token))
        {
        }

        private UnboundTokenException(string token, string message)
            : base(message)
        {
            Token = token;
        }

        public string Token { get; }

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