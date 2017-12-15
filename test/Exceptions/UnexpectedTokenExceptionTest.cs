using System;
using FluentAssertions;
using Nut.CommandLineParser.Exceptions;
using Xunit;

namespace Nut.CommandLineParser.Exceptions.Test
{ 
    public class UnexpectedTokenExceptionTest
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void ConstructorShouldThrowExceptionForIndexLowerThanZero(int index)
        {
            var exception = Assert.Throws<ArgumentException>(() =>
                new UnexpectedTokenException(index, "token")
            );
            exception.ParamName.Should().Be("index");
            exception.Message.Should().Be("Value cannot be negative.\r\nParameter name: index");
        }

        [Fact]
        public void ConstructorShouldThrowExceptionForNullToken()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new UnexpectedTokenException(0, null)
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
                new UnexpectedTokenException(0, token)
            );
            exception.ParamName.Should().Be("token");
            exception.Message.Should().Be("Value cannot be empty.\r\nParameter name: token");
        }

        [Theory]
        [InlineData(0, "x", "Unexpected token x at index 0.")]
        [InlineData(5, "test", "Unexpected token test at index 5.")]
        [InlineData(int.MaxValue, "something wrong", "Unexpected token something wrong at index 2147483647.")]
        public void MessagePropertyShouldHaveHelpfulMessage(int index, string token, string exceptedMessage) 
        {
            var exception = new UnexpectedTokenException(index, token);
            exception.Index.Should().Be(index);
            exception.Token.Should().Be(token);
            exception.Message.Should().Be(exceptedMessage);
        }
    }
}