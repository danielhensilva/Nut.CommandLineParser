using System;
using FluentAssertions;
using Xunit;
using Nut.CommandLineParser.Attributes;
using Nut.CommandLineParser.Specialized;

namespace Nut.CommandLineParser.Specialized.Test
{
    public class ObjectParserTest
    {
        public class PropertylessClass
        {
        }

        public class NoOptionAttributeClass
        {
            public string NoAttributeItem { get; set; }
        }

        [Fact]
        public void ShouldDoNothingForPropertylessClass() 
        {
            var parser = new ObjectParser<PropertylessClass>();
            var parsed = parser.Parse("anything");
            parsed.Should().NotBeNull();
            parsed.Should().BeOfType<PropertylessClass>();
        }

        [Fact]
        public void ShouldDoNothingForNoOptionAttributeClass() 
        {
            var parser = new ObjectParser<NoOptionAttributeClass>();
            var parsed = parser.Parse("anything");
            parsed.Should().NotBeNull();
            parsed.Should().BeOfType<NoOptionAttributeClass>();
            parsed.NoAttributeItem.Should().BeNull();
        }
    }
}