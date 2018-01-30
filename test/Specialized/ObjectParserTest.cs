using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Nut.CommandLineParser.Attributes;
using Nut.CommandLineParser.Exceptions;
using Nut.CommandLineParser.Specialized;
using Xunit;

namespace Nut.CommandLineParser.Test.Specialized
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class ObjectParserTest
    {
        private class SingleOptionNameAttributeClass
        {
            [OptionName("key")]
            public string Item { get; set; }
        }

        private class SingleOptionAliasAttributeClass
        {
            [OptionAlias('k')]
            public string Item { get; set; }
        }

        private class MultipleOptionNameAttributeClass 
        {
            [OptionName("key")]
            public string Item1 { get; set; }
            
            [OptionName("Key")]
            public string Item2 { get; set; }
            
            [OptionName("KEY")]
            public string Item3 { get; set; }
        }

        private class MultipleOptionAliasAttributeClass
        {
            [OptionAlias('x')]
            public string Item1 { get; set; }

            [OptionAlias('X')]
            public string Item2 { get; set; }
        }

        private class CollisionOfOptionNameAttributeClass
        {
            [OptionName("foo")]
            public string Item1 { get; set; }
            
            [OptionName("foo")]
            public string Item2 { get; set; }
        }

        private class CollisionOfOptionAliasAttributeClass
        {
            [OptionAlias('x')]
            public string Item1 { get; set; }

            [OptionAlias('x')]
            public string Item2 { get; set; }
        }

        private class PrimitivePropertyForAttributeClass
        {
            [OptionAlias('a')]
            [OptionName("bool")]
            public bool ItemBool { get; set; }
            
            [OptionAlias('b')]
            [OptionName("char")]
            public char ItemChar { get; set; }
        }

        private class NumericPropertyForAttributeClass
        {
            [OptionAlias('a')]
            [OptionName("byte")]
            public byte ItemByte { get; set; }
            
            [OptionAlias('b')]
            [OptionName("sbyte")]
            public sbyte ItemSByte { get; set; }

            [OptionAlias('c')]
            [OptionName("int")]
            public int ItemInt { get; set; }

            [OptionAlias('d')]
            [OptionName("uint")]
            public uint ItemUInt { get; set; }
            
            [OptionAlias('e')]
            [OptionName("long")]
            public long ItemLong { get; set; }
            
            [OptionAlias('f')]
            [OptionName("ulong")]
            public ulong ItemULong { get; set; }
            
            [OptionAlias('g')]
            [OptionName("short")]
            public short ItemShort { get; set; }
            
            [OptionAlias('h')]
            [OptionName("ushort")]
            public ushort ItemUShort { get; set; }
        }

        private class DecimalPropertyForAttributeClass
        {            
            [OptionAlias('a')]
            [OptionName("float")]
            public float ItemFloat { get; set; }

            [OptionAlias('b')]
            [OptionName("double")]
            public double ItemDouble { get; set; }
            
            [OptionAlias('c')]
            [OptionName("decimal")]
            public decimal ItemDecimal { get; set; }
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
        [InlineData("--k bravo", "bravo")]
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
        [InlineData("Key=alpha KEY=bravo key=gama", "gama", "alpha", "bravo")]
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

        [Fact]
        public void ParseMethodShouldThrowErrorWhenThereAreDuplicatedNameOptions()
        {
            var parser = new ObjectParser<CollisionOfOptionNameAttributeClass>();
            var exception = Assert.Throws<DuplicatedOptionsException>(
                () => parser.Parse(null));
            exception.Duplications.Should().BeEquivalentTo(new[] {"foo"});
        }

        [Fact]
        public void ParseMethodShouldThrowErrorWhenThereAreDuplicatedAliasOptions()
        {
            var parser = new ObjectParser<CollisionOfOptionAliasAttributeClass>();
            var exception = Assert.Throws<DuplicatedOptionsException>(
                () => parser.Parse(null));
            exception.Duplications.Should().BeEquivalentTo(new[] {"x"});
        }
        
        [Theory]
        [InlineData("a=true b=w", true, 'w')]
        [InlineData("b=x a=false", false, 'x')]
        [InlineData("bool=False char=y", false, 'y')]
        [InlineData("char=z bool=True", true, 'z')]
        [InlineData("bool=1 char=q", true, 'q')]
        [InlineData("char=p bool=0", false, 'p')]
        public void ParseMethodShouldParsePrimitiveAttributes(string args, bool expectedBool, char expectedChar) 
        {
            var parser = new ObjectParser<PrimitivePropertyForAttributeClass>();
            var parsed = parser.Parse(args);
            parsed.Should().NotBeNull();
            parsed.Should().BeOfType<PrimitivePropertyForAttributeClass>();
            parsed.ItemBool.Should().Be(expectedBool);
            parsed.ItemChar.Should().Be(expectedChar);
        }
        
        [Theory]
        [InlineData("a=1 b=2 c=3 d=4 e=5 f=6 g=7 h=8", 1, 2, 3, 4, 5, 6, 7, 8)]
        [InlineData("byte=11 sbyte=22 int=33 uint=44 long=55 ulong=66 short=77 ushort=88", 11, 22, 33, 44, 55, 66, 77, 88)]
        public void ParseMethodShouldParseNumericAttributes(
                string args, 
                byte expectedByte, 
                sbyte expectedSByte, 
                int expectedInt, 
                uint expectedUInt,
                long expectedLong, 
                ulong expectedULong, 
                short expectedShort, 
                ushort expectedUShort) 
        {
            var parser = new ObjectParser<NumericPropertyForAttributeClass>();
            var parsed = parser.Parse(args);
            parsed.Should().NotBeNull();
            parsed.Should().BeOfType<NumericPropertyForAttributeClass>();
            parsed.ItemByte.Should().Be(expectedByte);
            parsed.ItemSByte.Should().Be(expectedSByte);
            parsed.ItemInt.Should().Be(expectedInt);
            parsed.ItemUInt.Should().Be(expectedUInt);
            parsed.ItemLong.Should().Be(expectedLong);
            parsed.ItemULong.Should().Be(expectedULong);
            parsed.ItemShort.Should().Be(expectedShort);
            parsed.ItemUShort.Should().Be(expectedUShort);
        }
        
//        [Theory]
//        [InlineData("x=1.1 y=2.2 z=3.3", 1.1, 2.2, 3.3)]
//        [InlineData("float=44.4 double=55.5 decimal=66.6", 44.4, 55.5, 66.6)]
        public void ParseMethodShouldParseDecimalAttributes(
            string args, 
            float expectedFloat,
            double expectedDouble,
            decimal expectedDecimal) 
        {
            var parser = new ObjectParser<DecimalPropertyForAttributeClass>();
            var parsed = parser.Parse(args);
            parsed.Should().NotBeNull();
            parsed.Should().BeOfType<DecimalPropertyForAttributeClass>();
            parsed.ItemFloat.Should().Be(expectedFloat);
            parsed.ItemDouble.Should().Be(expectedDouble);
            parsed.ItemDecimal.Should().Be(expectedDecimal);
        }
    }
}