using System;
using FluentAssertions;
using Nut.CommandLineParser.Models;
using Xunit;

namespace Nut.CommandLineParser.Test.Models
{
    public class ArgKeyValuePairTest
    {
        [Theory]
        [InlineData("key", "value")]
        [InlineData("alpha", "bravo")]
        public void PropertiesShouldHaveGettersAndSetters(string key, string value)
        {
            var pair = new ArgKeyValuePair(key, value);
            pair.Key.Should().Be(key);
            pair.Value.Should().Be(value);

            var updatedKey = string.Concat(key, "updated");
            pair.Key = updatedKey;
            pair.Key.Should().Be(updatedKey);

            var updatedValue = string.Concat(value, "updated");
            pair.Value = updatedValue;
            pair.Value.Should().Be(updatedValue);
        }

        [Fact]
        public void PropertyKeyShouldThrowExceptionForNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new ArgKeyValuePair(null, "value")
            );

            exception.ParamName.Should().Be("key");
            exception.Message.Should().Be("Value cannot be null.\r\nParameter name: key");
        }
    }
}