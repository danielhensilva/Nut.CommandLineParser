using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Nut.CommandLineParser.Models;
using Xunit;

namespace Nut.CommandLineParser.Test.Models
{
    public class ArgKeyValuePairsTest
    {
        [Fact]
        public void ParameterlessConstructorShouldInitializeEmptyCollection()
        {
            var pairs = new ArgKeyValuePairs();
            pairs.Count.Should().Be(0);
            pairs.Any().Should().BeFalse();
        }

        [Theory]
        [InlineData(new[] {"one", "two", "three"}, new[] {"four", "six", "seven"})]
        [InlineData(new[] {"alpha", "bravo", "charlie"}, new[] {"delta", "echo", "falcon"})]
        public void CollectionConstructorShouldInitializeWithGivenCollection(string[] keys, string[] values)
        {
            var collection = new List<ArgKeyValuePair>();
            for (var i = 0; i < keys.Length; i++)
                collection.Add(new ArgKeyValuePair(keys[i], values[i]));

            var pairs = new ArgKeyValuePairs(collection);
            pairs.Count.Should().Be(collection.Count);
            pairs.Any().Should().BeTrue();
            
            for (var i = 0; i < pairs.Count; i++)
            {
                var pair = pairs[i];
                pair.Key.Should().Be(keys[i]);
                pair.Value.Should().Be(values[i]);
                pair.Should().BeSameAs(collection[i]);
            }
        }
    }
}