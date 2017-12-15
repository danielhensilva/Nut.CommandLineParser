using System;
using System.Linq;
using FluentAssertions;
using Nut.CommandLineParser.Attributes;
using Xunit;

namespace Nut.CommandLineParser.Attributes.Test
{
    public class OptionAliasAttributeTest
    {
        [Fact]
        public void ShouldBeAvailableToPropertiesAndNotInheritedAndAllowMultiple() 
        {
            var attribute = new OptionAliasAttribute('a');
            var customAttributes = attribute.GetType().GetCustomAttributes(false);
            var customAttributeUsage = customAttributes.OfType<AttributeUsageAttribute>().First() as AttributeUsageAttribute;
            customAttributeUsage.ValidOn.Should().Be(AttributeTargets.Property);
            customAttributeUsage.Inherited.Should().BeFalse();
            customAttributeUsage.AllowMultiple.Should().BeTrue();
        }

        [Theory]
        [InlineData(' ')]
        [InlineData('!')]
        [InlineData('%')]
        public void ConstructorShouldThrowExceptionIfAliasIsNotDigitAndNotLetter(char alias) 
        {
            var exception = Assert.Throws<ArgumentException>(() =>
                new OptionAliasAttribute(alias)
            );
            exception.ParamName.Should().Be("alias");
            exception.Message.Should().Be("Value can only be letter or digit.\r\nParameter name: alias");
        }
        
        [Theory]
        [InlineData('a')]
        [InlineData('y')]
        public void ConstructorShouldSetNameProperly(char alias) 
        {
            var attribute = new OptionAliasAttribute(alias);
            attribute.Alias.Should().Be(alias);
        }
    }
}