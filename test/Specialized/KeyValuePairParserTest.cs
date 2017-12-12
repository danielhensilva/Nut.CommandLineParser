using System;
using FluentAssertions;
using Xunit;
using Nut.CommandLineParser.Exceptions;

namespace Nut.CommandLineParser.Specialized.Test
{
    public class KeyValuePairParserTest
    {
        [Fact]
        public void ParseMethodShouldThrowArgumentNullExceptionWhenArgsAreNull()
        {
            string args = null;
            var exception = Assert.Throws<ArgumentNullException>(() => 
                new KeyValuePairParser().Parse(args)
            );
            exception.Message.Should().StartWith("Value cannot be null");
            exception.ParamName.Should().Be("args");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void ParseMethodShouldReturnEmptyCollectionWhenArgsAreEmptyString(string args)
        {
            var collection = new KeyValuePairParser().Parse(args);
            collection.Should().NotBeNull();
            collection.Length.Should().Be(0);
        }

        [Theory]
        [InlineData("=", "=")]
        [InlineData("--", "--")]
        [InlineData("alpha=beta=gama", "=")]
        [InlineData("--missingValue", "--")]
        [InlineData("this=that -any -thing", "-")]
        public void ParseMethodShouldThrowErrorForInvalidSingleKeyValueParameter(string invalidArgs, string expectedToken)
        {
            var exception = Assert.Throws<UnexpectedTokenException>(() => 
                new KeyValuePairParser().Parse(invalidArgs)
            );
            exception.Token.Should().Be(expectedToken);
        }

        [Theory]
        [InlineData("foo=bar", "foo", "bar")]
        [InlineData("key=value", "key", "value")]
        [InlineData("alpha=beta", "alpha", "beta")]
        public void ParseMethodShouldParseSingleKeyValueParameterWithEqualsSign(string args, string expectedKey, string expectedValue)
        {
            var collection = new KeyValuePairParser().Parse(args);
            collection.Should().NotBeNull();
            collection.Length.Should().Be(1);
            collection[0].Key.Should().Be(expectedKey);
            collection[0].Value.Should().Be(expectedValue);
        }

        [Theory]
        [InlineData("-foo bar", "foo", "bar")]
        [InlineData("-key value", "key", "value")]
        [InlineData("-alpha beta", "alpha", "beta")]
        public void ParseMethodShouldParseSingleKeyValueParameterWithSingleSlash(string args, string expectedKey, string expectedValue)
        {
            var collection = new KeyValuePairParser().Parse(args);
            collection.Should().NotBeNull();
            collection.Length.Should().Be(1);
            collection[0].Key.Should().Be(expectedKey);
            collection[0].Value.Should().Be(expectedValue);
        }

        [Theory]
        [InlineData("--foo bar", "foo", "bar")]
        [InlineData("--key value", "key", "value")]
        [InlineData("--alpha beta", "alpha", "beta")]
        public void ParseMethodShouldParseSingleKeyValueParameterWithDoubleSlash(string args, string expectedKey, string expectedValue)
        {
            var collection = new KeyValuePairParser().Parse(args);
            collection.Should().NotBeNull();
            collection.Length.Should().Be(1);
            collection[0].Key.Should().Be(expectedKey);
            collection[0].Value.Should().Be(expectedValue);
        }
    }
}
