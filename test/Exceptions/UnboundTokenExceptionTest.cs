using System;
using FluentAssertions;
using Nut.CommandLineParser.Exceptions;
using Xunit;

namespace Nut.CommandLineParser.Test.Exceptions
{ 
    public class UnboundTokenExceptionTest
    {
        [Fact]
        public void ConstructorShouldThrowExceptionForNullToken()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new UnboundTokenException(null)
            );
            exception.ParamName.Should().Be("token");
            exception.Message.Should().Be("Value cannot be null.\r\nParameter name: token");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        public void ConstructorShouldThrowExceptionForEmptyOrWhitespaceToken(string token)
        {
            var exception = Assert.Throws<ArgumentException>(() =>
                new UnboundTokenException(token)
            );
            exception.ParamName.Should().Be("token");
            exception.Message.Should().Be("Value cannot be empty.\r\nParameter name: token");
        }

        [Theory]
        [InlineData("x", "Unbound token x.")]
        [InlineData("test", "Unbound token test.")]
        [InlineData("something wrong", "Unbound token something wrong.")]
        public void MessagePropertyShouldHaveHelpfulMessage(string token, string exceptedMessage) 
        {
            var exception = new UnboundTokenException(token);
            exception.Token.Should().Be(token);
            exception.Message.Should().Be(exceptedMessage);
        }
    }
}