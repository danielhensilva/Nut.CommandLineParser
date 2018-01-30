using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Nut.CommandLineParser.Models;
using Xunit;

namespace Nut.CommandLineParser.Test.Models
{
    public class PropertyOptionPairsTest
    {
        [Fact]
        public void ParameterlessConstructorShouldInitializeEmptyCollection()
        {
            var pairs = new PropertyOptionPairs();
            pairs.Count.Should().Be(0);
            pairs.Any().Should().BeFalse();
        }

        [Theory]
        [InlineData("one")]
        [InlineData("alpha", "bravo", "charlie")]
        public void CollectionConstructorShouldInitializeWithGivenCollection(params string[] options)
        {
            var propertyName = nameof(PropertyOptionPair.Option);
            var property = typeof(PropertyOptionPair).GetProperty(propertyName);
            var collection = new List<PropertyOptionPair>();

            foreach (var option in options)
                collection.Add(new PropertyOptionPair(property, option));

            var pairs = new PropertyOptionPairs(collection);
            pairs.Count.Should().Be(collection.Count);
            pairs.Any().Should().BeTrue();
            
            for (var i = 0; i < pairs.Count; i++)
            {
                var pair = pairs[i];
                pair.Option.Should().Be(options[i]);
                pair.Property.Should().BeSameAs(property);
                pair.Should().BeSameAs(collection[i]);
            }
        }

        [Theory]
        [InlineData(new string[0], "")]
        [InlineData(new string[0], null)]
        [InlineData(new string[0], "any")]
        [InlineData(new[] { "" }, null)]
        [InlineData(new[] { "" }, "any")]
        [InlineData(new[] { "one" }, "")]
        [InlineData(new[] { "one" }, null)]
        [InlineData(new[] { "one" }, "any")]
        [InlineData(new[] { "falcon" }, "charlie")]
        [InlineData(new[] { "alpha", "bravo", "charlie" }, "falcon")]
        public void MethodTryFindByOptionShouldReturnFalseAndOutputNullWhenKeyIsNotFound(string[] options, string searchTerm)
        {
            var propertyName = nameof(PropertyOptionPair.Option);
            var property = typeof(PropertyOptionPair).GetProperty(propertyName);
            var collection = new List<PropertyOptionPair>();

            foreach (var option in options)
                collection.Add(new PropertyOptionPair(property, option));

            var pairs = new PropertyOptionPairs(collection);
            pairs.Count.Should().Be(collection.Count);
            
            var found = pairs.TryFindByOption(searchTerm, out var output);
            found.Should().BeFalse();
            output.Should().BeNull();
        }  

        [Theory]
        [InlineData(new[] { "two" }, "two")]
        [InlineData(new[] { "alpha", "bravo", "charlie" }, "alpha")]
        [InlineData(new[] { "alpha", "bravo", "charlie" }, "bravo")]
        [InlineData(new[] { "alpha", "bravo", "charlie" }, "charlie")]
        public void MethodTryFindByOptionShouldReturnTrueAndOutputValidWhenKeyIsFound(string[] options, string searchTerm)
        {
            var propertyName = nameof(PropertyOptionPair.Option);
            var property = typeof(PropertyOptionPair).GetProperty(propertyName);
            var collection = new List<PropertyOptionPair>();

            foreach (var option in options)
                collection.Add(new PropertyOptionPair(property, option));

            var pairs = new PropertyOptionPairs(collection);
            pairs.Count.Should().Be(collection.Count);
            pairs.Any().Should().BeTrue();
            
            var found = pairs.TryFindByOption(searchTerm, out var output);
            found.Should().BeTrue();
            output.Should().BeSameAs(property);
        }  
    }
}