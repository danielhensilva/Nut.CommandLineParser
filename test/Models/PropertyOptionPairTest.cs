using System;
using System.Reflection;
using FluentAssertions;
using Xunit;

namespace Nut.CommandLineParser.Models.Test
{
    public class PropertyOptionPairTest
    {
        [Fact]
        public void PropertyPropertyShouldThrowExceptionForNull() 
        {
            PropertyInfo property = null;

            var exception = Assert.Throws<ArgumentNullException>(() => 
                new PropertyOptionPair(property, "option")
            );

            exception.ParamName.Should().Be("property");
            exception.Message.Should().Be("Value cannot be null.\r\nParameter name: property");
        }

        [Theory]
        [InlineData("option")]
        [InlineData("alphabravo")]
        public void PropertiesShouldHaveGettersAndSetters(string option) 
        {
            var propertyName = nameof(PropertyOptionPair.Option);
            var property = typeof(PropertyOptionPair).GetProperty(propertyName);
            var pair = new PropertyOptionPair(property, option);

            pair.Property.Should().BeSameAs(property);
            pair.Option.Should().Be(option);

            var updatedProperty = typeof(PropertyOptionPair).GetProperty("Property");
            pair.Property = updatedProperty;
            pair.Property.Should().BeSameAs(updatedProperty);

            var updatedOption = string.Concat(option, "updated");
            pair.Option = updatedOption;
            pair.Option.Should().Be(updatedOption);
        }
    }
}