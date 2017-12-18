using System;
using FluentAssertions;
using Xunit;
using Nut.CommandLineParser.Attributes;
using Nut.CommandLineParser.Exceptions;
using Nut.CommandLineParser.Specialized;

namespace Nut.CommandLineParser.Specialized.Test
{
    public class ObjectParserTest
    {
        public class NoOptionAttributeClass
        {
            public string Item { get; set; }
        }

        public class SingleOptionNameAttributeClass
        {
            [OptionName("key")]
            public string Item { get; set; }
        }

        public class SingleOptionAliasAttributeClass
        {
            [OptionAlias('k')]
            public string Item { get; set; }
        }

        public class MultipleOptionNameAttributeClass 
        {
            [OptionName("key")]
            public string Item1 { get; set; }
            
            [OptionName("Key")]
            public string Item2 { get; set; }
            
            [OptionName("KEY")]
            public string Item3 { get; set; }
        }

        public class MultipleOptionAliasAttributeClass
        {
            [OptionAlias('x')]
            public string Item1 { get; set; }

            [OptionAlias('X')]
            public string Item2 { get; set; }
        }

        public class CollisionOfOptionNameAttributeClass
        {
            [OptionName("foo")]
            public string Item1 { get; set; }
            
            [OptionName("foo")]
            public string Item2 { get; set; }
        }

        public class CollisionOfOptionAliasAttributeClass
        {
            [OptionAlias('x')]
            public string Item1 { get; set; }

            [OptionAlias('x')]
            public string Item2 { get; set; }
        }

        [Fact]
        public void ParseMethodShouldThrowExceptionForNullArgs() 
        {
            string args = null;
            var exception = Assert.Throws<ArgumentNullException>(() => 
                new ObjectParser<object>().Parse(args)
            );
            exception.Message.Should().StartWith("Value cannot be null");
            exception.ParamName.Should().Be("args");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        public void ParseMethodShouldDoNothingForEmptyArgs(string args) 
        {
            var parser = new ObjectParser<object>();
            var parsed = parser.Parse(args);
            parsed.Should().NotBeNull();
            parsed.Should().BeOfType<object>();
        }

        [Theory]
        [InlineData("Item=Value")]
        [InlineData("--Item Value")]
        public void ParseMethodShouldThrowErrorForNotBoundArgs(string args) 
        {
            var exception = Assert.Throws<UnboundTokenException>(() => 
                new ObjectParser<object>().Parse(args)
            );
            exception.Message.Should().StartWith("Unbound token Item");
        }
        
        [Theory]
        [InlineData("key=value1", "value1")]
        [InlineData("key=foobar", "foobar")]
        [InlineData("--key power", "power")]
        [InlineData("--key value2", "value2")]
        public void ParseMethodShouldParseSingleOptionNameAttributeClass(string args, string expectedValue) 
        {
            var parser = new ObjectParser<SingleOptionNameAttributeClass>();
            var parsed = parser.Parse(args);
            parsed.Should().NotBeNull();
            parsed.Should().BeOfType<SingleOptionNameAttributeClass>();
            parsed.Item.Should().Be(expectedValue);
        }
        
        [Theory]
        [InlineData("k=this", "this")]
        [InlineData("k=that", "that")]
        [InlineData("--k beta", "beta")]
        [InlineData("--k alpha", "alpha")]
        public void ParseMethodShouldParseSingleOptionAliasAttributeClass(string args, string expectedValue) 
        {
            var parser = new ObjectParser<SingleOptionAliasAttributeClass>();
            var parsed = parser.Parse(args);
            parsed.Should().NotBeNull();
            parsed.Should().BeOfType<SingleOptionAliasAttributeClass>();
            parsed.Item.Should().Be(expectedValue);
        }
        
        [Theory]
        [InlineData("Key=alpha KEY=beta key=gama", "gama", "alpha", "beta")]
        [InlineData("key=value1 Key=value2 KEY=value3", "value1", "value2", "value3")]
        public void ParseMethodShouldParseMultipleOptionNameAttributeClass(string args, string expectedValue1, string expectedValue2, string expectedValue3) 
        {
            var parser = new ObjectParser<MultipleOptionNameAttributeClass>();
            var parsed = parser.Parse(args);
            parsed.Should().NotBeNull();
            parsed.Should().BeOfType<MultipleOptionNameAttributeClass>();
            parsed.Item1.Should().Be(expectedValue1);
            parsed.Item2.Should().Be(expectedValue2);
            parsed.Item3.Should().Be(expectedValue3);
        }
        
        [Theory]
        [InlineData("X=foo x=bar", "bar", "foo")]
        [InlineData("x=value1 X=value2", "value1", "value2")]
        public void ParseMethodShouldParseMultipleOptionAliasAttributeClass(string args, string expectedValue1, string expectedValue2) 
        {
            var parser = new ObjectParser<MultipleOptionAliasAttributeClass>();
            var parsed = parser.Parse(args);
            parsed.Should().NotBeNull();
            parsed.Should().BeOfType<MultipleOptionAliasAttributeClass>();
            parsed.Item1.Should().Be(expectedValue1);
            parsed.Item2.Should().Be(expectedValue2);
        }
    }
}