using System;
using FluentAssertions;
using Nut.CommandLineParser.Models;
using Xunit;

namespace Nut.CommandLineParser.Test.Models
{
    public class PropertyOptionPairTest
    {
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

        [Fact]
        public void PropertyPropertyShouldThrowExceptionForNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new PropertyOptionPair(null, "option")
            );

            exception.ParamName.Should().Be(nameof(PropertyOptionPair.Property));
            exception.Message.Should().Be("Value cannot be null.\r\nParameter name: Property");
        }
    }
}