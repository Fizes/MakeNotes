using System.Linq;
using MakeNotes.Common.Infrastructure.Extensions;
using Xunit;

namespace MakeNotes.UnitTests.Common
{
    public class StringExtensionsTests
    {
        [Fact]
        public void ToNameValueCollection_ShouldReturnNameValueCollection_WhenStringGiven()
        {
            var source = "test key=test;testkey=test value";

            var result = source.ToNameValueCollection();
            var value1 = result.Get("test key");
            var value2 = result.Get("testkey");

            Assert.Equal("test", value1);
            Assert.Equal("test value", value2);
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("test string")]
        public void ToNameValueCollection_ShouldReturnGivenValue_WhenNoKeyValuePairGiven(string source)
        {
            var result = source.ToNameValueCollection();

            Assert.Single(result);
            Assert.Null(result.AllKeys.First());
            Assert.Equal(source, result.Get(0));
        }

        [Fact]
        public void ToNameValueCollection_ShouldReturnEmptyNameValueCollection_WhenEmptyStringGiven()
        {
            var source = "";

            var result = source.ToNameValueCollection();

            Assert.Empty(result);
        }
        
        [Fact]
        public void ToNameValueCollection_ShouldReturnEmptyNameValueCollection_WhenEmptyStringArrayGiven()
        {
            var source = new string[0];

            var result = source.ToNameValueCollection();

            Assert.Empty(result);
        }

        [Fact]
        public void ToNameValueCollection_ShouldReturnNameValueCollection_WhenStringAndSeparatorGiven()
        {
            var separator = '.';
            var source = $"test key=test{separator}testkey=test value";

            var result = source.ToNameValueCollection(separator);
            var value1 = result.Get("test key");
            var value2 = result.Get("testkey");

            Assert.Equal("test", value1);
            Assert.Equal("test value", value2);
        }

        [Fact]
        public void ToNameValueCollection_ShouldReturnNameValueCollection_WhenStringArrayGiven()
        {
            var source = new[]
            {
                "test key=test",
                "testkey=test value"
            };

            var result = source.ToNameValueCollection();
            var value1 = result.Get("test key");
            var value2 = result.Get("testkey");

            Assert.Equal("test", value1);
            Assert.Equal("test value", value2);
        }

        [Fact]
        public void ToKeyValuePairString_ShouldReturnStringAsKeyValuePair()
        {
            var key = "testkey";
            var value = "value";

            var result = StringExtensions.ToKeyValuePairString(key, value);

            Assert.Equal($"{key}={value}", result);
        }
    }
}
