using System;
using FluentAssertions;
using Nut.CommandLineParser.Extensions;
using Xunit;

namespace Nut.CommandLineParser.Extensions.Test
{
    public class StringExtensionsTest 
    {
        [Theory]
        [InlineData(null, false)]
        [InlineData("bogus", false)]
        [InlineData(" . ", false)]
        [InlineData("   ", true)]
        [InlineData(" ", true)]
        [InlineData("", true)]
        public void IsWhitespaceMethod(string input, bool expectedOutput) 
        {
            input.IsEmptyOrWhitespace().Should().Be(expectedOutput);
        }
        
        [Theory]
        [InlineData(null, null, null)]
        [InlineData("", null, "")]
        [InlineData("", "", "")]
        [InlineData(" ", " ", "")]
        [InlineData("a", "a", "")]
        [InlineData("b", "b", "")]
        [InlineData("c", "c", "")]
        [InlineData("abcdef", "a", "bcdef")]
        [InlineData("abcdef", "ab", "cdef")]
        [InlineData("abcdef", "abc", "def")]
        [InlineData("abcdef", "b", "acdef")]
        [InlineData("abcdef", "cd", "abef")]
        [InlineData("abcdef", "f", "abcde")]
        [InlineData("abcdef", "ef", "abcd")]
        [InlineData("abcdef", "def", "abc")]
        [InlineData("alpha bravo charlie delta alpha bravo charlie delta", "bravo ", "alpha charlie delta alpha charlie delta")]
        [InlineData("alpha bravo charlie delta alpha bravo charlie delta", " charlie delta", "alpha bravo alpha bravo")]
        public void RemoveAllMethod(string input, string param, string expectedOutput) 
        {
            input.RemoveAll(param).Should().Be(expectedOutput);
        }
        
        [Theory]
        [InlineData(null, null, null)]
        [InlineData("", null, "")]
        [InlineData("", "", "")]
        [InlineData(" ", " ", "")]
        [InlineData("a", "a", "")]
        [InlineData("b", "b", "")]
        [InlineData("c", "c", "")]
        [InlineData("abcdef", "a", "bcdef")]
        [InlineData("abcdef", "ab", "cdef")]
        [InlineData("abcdef", "abc", "def")]
        [InlineData("abcdef", "b", "acdef")]
        [InlineData("abcdef", "cd", "abef")]
        [InlineData("abcdef", "f", "abcde")]
        [InlineData("abcdef", "ef", "abcd")]
        [InlineData("abcdef", "def", "abc")]
        [InlineData("alpha bravo charlie delta alpha bravo charlie delta", "bravo ", "alpha charlie delta alpha bravo charlie delta")]
        [InlineData("alpha bravo charlie delta alpha bravo charlie delta", " charlie delta", "alpha bravo alpha bravo charlie delta")]
        public void RemoveFirstOccurrenceMethod(string input, string param, string expectedOutput) 
        {
            input.RemoveFirstOccurrence(param).Should().Be(expectedOutput);
        }
        
        [Theory]
        [InlineData(null, null, null)]
        [InlineData("", null, "")]
        [InlineData("", "", "")]
        [InlineData(" ", " ", "")]
        [InlineData("a", "a", "")]
        [InlineData("b", "b", "")]
        [InlineData("c", "c", "")]
        [InlineData("abcdef", "a", "bcdef")]
        [InlineData("abcdef", "ab", "cdef")]
        [InlineData("abcdef", "abc", "def")]
        [InlineData("abcdef", "b", "acdef")]
        [InlineData("abcdef", "cd", "abef")]
        [InlineData("abcdef", "f", "abcde")]
        [InlineData("abcdef", "ef", "abcd")]
        [InlineData("abcdef", "def", "abc")]
        [InlineData("alpha bravo charlie delta alpha bravo charlie delta", "bravo ", "alpha bravo charlie delta alpha charlie delta")]
        [InlineData("alpha bravo charlie delta alpha bravo charlie delta", " charlie delta", "alpha bravo charlie delta alpha bravo")]
        public void RemoveLastOccurrenceMethod(string input, string param, string expectedOutput) 
        {
            input.RemoveLastOccurrence(param).Should().Be(expectedOutput);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("  ", null)]
        [InlineData(" ", null)]
        [InlineData("", null)]
        [InlineData("alpha bravo", "alpha")]
        [InlineData("alpha bravo charlie", "alpha")]
        [InlineData(" bogus ", "bogus")]
        [InlineData("  alpha bravo ", "alpha")]
        public void FirstWordOrDefaultMethod(string input, string expectedOutput)
        {
            input.FirstWordOrDefault().Should().Be(expectedOutput);
        }
    }
}