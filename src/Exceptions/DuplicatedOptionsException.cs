﻿using System;

namespace Nut.CommandLineParser.Exceptions
{
    public class DuplicatedOptionsException : Exception
    {
        public DuplicatedOptionsException(string[] duplications)
            : this(duplications, GenerateMessage(duplications))
        {
        }

        private DuplicatedOptionsException(string[] duplications, string message)
            : base(message)
        {
            Duplications = duplications;
        }

        public string[] Duplications { get; }

        private static string GenerateMessage(string[] duplications)
        {
            if (duplications == null)
                throw new ArgumentNullException(nameof(duplications));

            if (duplications.Length == 0)
                throw new ArgumentException("Value cannot be an empty collection.", nameof(duplications));

            var elements = string.Join(", ", duplications);
            return $"Duplicated keys are found: [{elements}].";
        }
    }
}