using System;
using Nut.CommandLineParser.Extensions;

namespace Nut.CommandLineParser.Exceptions
{
    public class UnexpectedTokenException : Exception
    {
        public int Index { get; set; }

        public string Token { get; set; }

        public UnexpectedTokenException(int index, string token)
            : this(index, token, UnexpectedTokenException.GenerateMessage(index, token))
        {
        }

        private UnexpectedTokenException(int index, string token, string message)
            : base(message) 
        {
            this.Index = index;
            this.Token = token;
        }

        private static string GenerateMessage(int index, string token)
        {
            if (index < 0)
                throw new ArgumentException("Value cannot be negative.", nameof(index));

            if (token == null)
                throw new ArgumentNullException(nameof(token));

            if (token.IsEmptyOrWhitespace())
                throw new ArgumentException("Value cannot be empty.", nameof(token));

            return $"Unexpected token {token} at index {index}.";
        }
    }
}