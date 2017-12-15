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
        [InlineData("=", "=", 0)]
        [InlineData("--", "--", 0)]
        [InlineData("alpha=beta=gama", "=gama", 10)]
        [InlineData("--missingValue", "--missingValue", 0)]
        [InlineData("this=that -any -thing", "-any", 10)]
        public void ParseMethodShouldThrowErrorForInvalidSingleKeyValueParameter(string invalidArgs, string expectedToken, int expectedIndex)
        {
            var exception = Assert.Throws<UnexpectedTokenException>(() => 
                new KeyValuePairParser().Parse(invalidArgs)
            );
            exception.Token.Should().Be(expectedToken);
            exception.Index.Should().Be(expectedIndex);
        }

        [Theory]
        [InlineData("foo=bar", "foo", "bar")]
        [InlineData("key=value", "key", "value")]
        [InlineData("alpha=beta", "alpha", "beta")]
        [InlineData("element=\"single\"", "element", "single")]
        [InlineData("this=\"multi text\"", "this", "multi text")]
        [InlineData("anotherKey=\"very long 4 multi text\"", "anotherKey", "very long 4 multi text")]
        public void ParseMethodShouldParseSingleKeyValueParameterWithEqualsSign(string args, string expectedKey, string expectedValue)
        {
            var collection = new KeyValuePairParser().Parse(args);
            collection.Should().NotBeNull();
            collection.Length.Should().Be(1);
            collection[0].Key.Should().Be(expectedKey);
            collection[0].Value.Should().Be(expectedValue);
        }

        [Theory]
        [InlineData("-f bar", "f", "bar")]
        [InlineData("-a beta", "a", "beta")]
        [InlineData("-k value", "k", "value")]
        [InlineData("-e \"single\"", "e", "single")]
        [InlineData("-x \"multi text\"", "x", "multi text")]
        [InlineData("-t \"another long 10 multi text message\"", "t", "another long 10 multi text message")]
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
        [InlineData("--element \"single\"", "element", "single")]
        [InlineData("--this \"multi text\"", "this", "multi text")]
        [InlineData("--anotherKey \"very long 4 multi text\"", "anotherKey", "very long 4 multi text")]
        public void ParseMethodShouldParseSingleKeyValueParameterWithDoubleSlash(string args, string expectedKey, string expectedValue)
        {
            var collection = new KeyValuePairParser().Parse(args);
            collection.Should().NotBeNull();
            collection.Length.Should().Be(1);
            collection[0].Key.Should().Be(expectedKey);
            collection[0].Value.Should().Be(expectedValue);
        }

        [Theory]
        [InlineData("foo=bar alpha=beta", 2, 
            new[] { "foo", "alpha" }, 
            new[] { "bar", "beta" })]
        [InlineData("alpha=beta gama=\"new value set\"", 2, 
            new[] { "alpha", "gama" }, 
            new[] { "beta", "new value set" })]
        [InlineData("alpha=beta gama=delta foo=bar key=value", 4, 
            new[] { "alpha", "gama", "foo", "key" }, 
            new[] { "beta", "delta", "bar", "value" })]
        public void ParseMethodShouldParseDoubleKeyValueParameterWithEqualsSign(string args, int expectedLength, string[] expectedKeys, string[] expectedValues)
        {
            var collection = new KeyValuePairParser().Parse(args);
            collection.Should().NotBeNull();
            collection.Length.Should().Be(expectedLength);

            for (int i = 0; i < collection.Length; i++) 
            {
                collection[i].Key.Should().Be(expectedKeys[i]);
                collection[i].Value.Should().Be(expectedValues[i]);
            }
        }

        [Theory]
        [InlineData("-f bar -k value", 2, 
            new[] { "f", "k" }, 
            new[] { "bar", "value" })]
        [InlineData("-x abc -o \"c:\\Projects\\\"", 2, 
            new[] { "x", "o" }, 
            new[] { "abc", "c:\\Projects\\" })]
        [InlineData("-e echo -g gama -k value", 3, 
            new[] { "e", "g", "k" }, 
            new[] { "echo", "gama", "value" })]
        [InlineData("-a beta -f bar -k value -g delta -f echo", 5, 
            new[] { "a", "f", "k", "g", "f" }, 
            new[] { "beta", "bar", "value", "delta", "echo" })]
        public void ParseMethodShouldParseMultipleKeyValueParameterWithSingleSlash(string args, int expectedLength, string[] expectedKeys, string[] expectedValues)
        {
            var collection = new KeyValuePairParser().Parse(args);
            collection.Should().NotBeNull();
            collection.Length.Should().Be(expectedLength);

            for (int i = 0; i < collection.Length; i++) 
            {
                collection[i].Key.Should().Be(expectedKeys[i]);
                collection[i].Value.Should().Be(expectedValues[i]);
            }
        }

        [Theory]
        [InlineData("--foo bar --key value", 2, 
            new[] { "foo", "key" }, 
            new[] { "bar", "value" })]
        [InlineData("--foo \"bar bogus\" --key value", 2, 
            new[] { "foo", "key" }, 
            new[] { "bar bogus", "value" })]
        [InlineData("--charlie echo --gama delta --this that", 3, 
            new[] { "charlie", "gama", "this" },
            new[] { "echo", "delta", "that" })]
        [InlineData("--alpha beta --foo bar --gama delta --key value", 4, 
            new[] { "alpha", "foo", "gama", "key" }, 
            new[] { "beta", "bar", "delta", "value" })]
        public void ParseMethodShouldParseMultipleKeyValueParameterWithDoubleSlash(string args, int expectedLength, string[] expectedKeys, string[] expectedValues)
        {
            var collection = new KeyValuePairParser().Parse(args);
            collection.Should().NotBeNull();
            collection.Length.Should().Be(expectedLength);

            for (int i = 0; i < collection.Length; i++) 
            {
                collection[i].Key.Should().Be(expectedKeys[i]);
                collection[i].Value.Should().Be(expectedValues[i]);
            }
        }

        [Theory]
        [InlineData("method foo1=bar1 --foo2 bar2 -f bar3", 4, 
            new[] { "method", "foo1", "foo2", "f" }, 
            new[] { null, "bar1", "bar2", "bar3" })]
        [InlineData("\"one method\" foo1=\"bar1 par1\" --foo2 \"bar2 par2\" -f \"bar3 par3\"", 4, 
            new[] { "one method", "foo1", "foo2", "f" }, 
            new[] { null, "bar1 par1", "bar2 par2", "bar3 par3" })]
        [InlineData("fk=fv test -x xv1 --fk1 fv1 test2 fk2=fv2 -y xv2 --fk2 fv2", 8, 
            new[] { "fk", "test", "x", "fk1", "test2", "fk2", "y", "fk2" }, 
            new[] { "fv", null, "xv1", "fv1", null, "fv2", "xv2", "fv2" })]
        public void ParseMethodShouldBeAbleToParseDistinctTypes(string args, int expectedLength, string[] expectedKeys, string[] expectedValues)
        {
            var collection = new KeyValuePairParser().Parse(args);
            collection.Should().NotBeNull();
            collection.Length.Should().Be(expectedLength);

            for (int i = 0; i < collection.Length; i++) 
            {
                collection[i].Key.Should().Be(expectedKeys[i]);
                collection[i].Value.Should().Be(expectedValues[i]);
            }
        }
    }
}
