using System;
using System.Linq;
using FluentAssertions;
using Nut.CommandLineParser.Attributes;
using Xunit;

namespace Nut.CommandLineParser.Attributes.Test
{
    public class OptionNameAttributeTest
    {
        [Fact]
        public void ShouldBeAvailableToPropertiesAndNotInheritedAndAllowMultiple() 
        {
            var attribute = new OptionNameAttribute("anything");
            var customAttributes = attribute.GetType().GetCustomAttributes(false);
            var customAttributeUsage = customAttributes.OfType<AttributeUsageAttribute>().First() as AttributeUsageAttribute;
            customAttributeUsage.ValidOn.Should().Be(AttributeTargets.Property);
            customAttributeUsage.Inherited.Should().BeFalse();
            customAttributeUsage.AllowMultiple.Should().BeTrue();
        }

        [Fact]
        public void ConstructorShouldThrowExceptionIfNameIsNull() 
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new OptionNameAttribute(null)
            );
            exception.ParamName.Should().Be("name");
            exception.Message.Should().Be("Value cannot be null.\r\nParameter name: name");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        public void ConstructorShouldThrowExceptionIfNameIsEmptyOrWhitespace(string name) 
        {
            var exception = Assert.Throws<ArgumentException>(() =>
                new OptionNameAttribute(name)
            );
            exception.ParamName.Should().Be("name");
            exception.Message.Should().Be("Value cannot be empty.\r\nParameter name: name");
        }

        [Theory]
        [InlineData("a")]
        [InlineData("x")]
        [InlineData("y")]
        public void ConstructorShouldThrowExceptionIfNameIsOneCharLength(string name) 
        {
            var exception = Assert.Throws<ArgumentException>(() =>
                new OptionNameAttribute(name)
            );
            exception.ParamName.Should().Be("name");
            exception.Message.Should().Be("Value cannot be one char length. Consider using OptionAliasAttribute class instead.\r\nParameter name: name");
        }

        [Theory]
        [InlineData("this")]
        [InlineData("something")]
        public void ConstructorShouldSetNameProperly(string name) 
        {
            var attribute = new OptionNameAttribute(name);
            attribute.Name.Should().Be(name);
        }
    }
}